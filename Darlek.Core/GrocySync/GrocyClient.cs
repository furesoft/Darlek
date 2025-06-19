using Darlek.Core.GrocySync.Models;
using LiteDB;
using RestSharp;
using Spectre.Console;
using System;
using System.IO;
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
        httpClient.DefaultRequestHeaders.Add("GROCY-API-KEY", "5hRZXgi2XXYlNRi7WLijE7ilE8sxXWpoGEiNq6QzyJyfKTVnxm");
        httpClient.BaseAddress = new Uri("https://erp.furesoft.de/public/index.php/api/");
        httpClient.DefaultRequestHeaders.Add("Accept", "*/*");

        client = new RestClient(httpClient);
    }

    public void AddRecipe(BsonDocument recipe)
    {
        var id = CreateRecipe(recipe);
        //SetSourceUrl(id, recipe["url"].AsString); // doesn't work
    }

    private int CreateRecipe(BsonDocument recipe)
    {
        var pictureFileName = UploadRecipeImage(recipe);

        var body = new CreateRecipe
        {
            name = recipe["name"].AsString,
            description = recipe["content"].AsString,
            picture_file_name = pictureFileName
        };

        var request = new RestRequest("/objects/recipes");
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);

        var id = client.Post<CreatedResponse>(request).created_object_id;

        Console.WriteLine("Recipe synced");

        return id;
    }

    private string UploadRecipeImage(BsonDocument recipe)
    {
        var imgurl = recipe["imageuri"].AsString;
        var uri = new Uri(imgurl);
        var pictureFileName = uri.Segments[^1];

        var img = new WebClient().DownloadData(imgurl);

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arrow)
            .Start("Uploading Recipe Image...", ctx => {
                UploadRecipeImage(pictureFileName, img);
            });
        return pictureFileName;
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
}