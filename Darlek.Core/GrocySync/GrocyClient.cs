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

namespace Darlek.Core.GrocySync;

public class GrocyClient
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

    public void AddRecipe(Recipe recipe)
    {
        var id = CreateRecipe(recipe);
        //SetSourceUrl(id, recipe["url"].AsString); // doesn't work
    }

    private int CreateRecipe(Recipe recipe)
    {
        UploadRecipeImage(recipe);

        var body = new CreateRecipe
        {
            name = recipe.Name,
            description = recipe.Description,
            picture_file_name = recipe.PictureFileName
        };

        var request = new RestRequest("/objects/recipes");
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);

        var id = client.Post<CreatedResponse>(request).created_object_id;


        AnsiConsole.WriteLine("Recipe synced");

        return id;
    }

    private async void UploadRecipeImage(Recipe recipe)
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
}