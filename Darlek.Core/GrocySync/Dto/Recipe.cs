namespace Darlek.Core.GrocySync.Dto;

internal class CreateRecipe
{
    public string name { get; set; }
    public string description { get; set; }
    public string type { get; set; } = "normal";
    public string picture_file_name { get; set; }
}