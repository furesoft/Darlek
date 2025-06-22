using Darlek.Core.GrocySync.Dto;
using Darlek.Core.GrocySync.Models;
using LiteDB;
using RestSharp;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Darlek.Core.GrocySync;

internal class GrocyClient
{
    private RestClient client;
    private readonly HttpClient httpClient;

    public GrocyClient()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("GROCY-API-KEY", Config.Get("GROCY_APIKEY"));
        httpClient.BaseAddress = new Uri(Config.Get("GROCY_URL"));
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

        client = new RestClient(httpClient);
    }

    public async Task AddRecipe(Recipe recipe)
    {
        var id = await CreateRecipe(recipe);
        //SetSourceUrl(id, recipe["url"].AsString); // doesn't work
    }

    private async Task<int> CreateRecipe(Recipe recipe)
    {
        await UploadRecipeImage(recipe);

        var body = new CreateRecipe
        {
            name = recipe.Name,
            description = recipe.Description,
            picture_file_name = recipe.PictureFileName
        };

        var request = new RestRequest("/objects/recipes");
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);

        var id = (await client.PostAsync<CreatedResponse>(request)).created_object_id;

        AnsiConsole.WriteLine("Recipe created with ID: " + id);

        if (recipe.Ingredients.Any())
        {
            await AnsiConsole.Progress()
                .StartAsync(async ctx => {
                    var task = ctx.AddTask($"Adding Ingredients");
                    task.MaxValue(recipe.Ingredients.Count);

                    for (var index = 0; index < recipe.Ingredients.Count; index++)
                    {
                        var ingr = recipe.Ingredients[index];
                        await AddIngredientToRecipe(id, ingr);
                        task.Increment(1);
                    }
                });
        }

        return id;
    }

    private async Task UploadRecipeImage(Recipe recipe)
    {
        var img = await new HttpClient().GetByteArrayAsync(recipe.PictureUrl);

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arrow)
            .Start("Uploading Recipe Image...", ctx => {
                UploadRecipeImage(recipe.PictureFileName, img);
            });
    }

    private void SetSourceUrl(int id, string url)
    {
        var body = new RecipeUserFields
        {
            source = url
        };

        var request = new RestRequest($"/userfields/recipes/{id}");
        request.AddHeader("Content-Type", "application/json");
        var json = System.Text.Json.JsonSerializer.Serialize(body);
        request.AddStringBody(json, DataFormat.Json);

        var result = client.Post(request);
    }

    public void UploadRecipeImage(string filename, byte[] raw)
    {
        filename = Convert.ToBase64String(Encoding.UTF8.GetBytes(filename));

        var body = new ByteArrayContent(raw);
        body.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        _ = httpClient.PutAsync($"files/recipepictures/{filename}", body).Result;
    }

    public List<Product> GetAllProducts()
    {
        var request = new RestRequest("/objects/products");
        var response = client.Get<List<Product>>(request);
        return response;
    }

    public List<QuantityUnit> GetAllQuantityUnits()
    {
        var request = new RestRequest("/objects/quantity_units");
        var response = client.Get<List<QuantityUnit>>(request);
        return response;
    }

    private async Task AddIngredientToRecipe(int recipeId, Ingredient ingredient)
    {
        var ingrBody = new CreateRecipePos
        {
            RecipeId = recipeId,
            ProductId = ingredient.Product?.Id,
            Amount = ingredient.Measure.Quantity,
            QuantityUnitId = ingredient.Measure.QuantityUnit?.Id,
            IngredientGroup = ingredient.Group,
            VariableAmount = ingredient.Measure.VariableAmount,
        };

        var ingrRequest = new RestRequest("/objects/recipes_pos");
        ingrRequest.AddHeader("Content-Type", "application/json");
        ingrRequest.AddJsonBody(ingrBody);

        await client.PostAsync(ingrRequest);
    }
}