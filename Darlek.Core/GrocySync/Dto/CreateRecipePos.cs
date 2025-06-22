using System.Text.Json.Serialization;

namespace Darlek.Core.GrocySync.Dto;

public class CreateRecipePos
{
    [JsonPropertyName("recipe_id")]
    public int RecipeId { get; set; }

    [JsonPropertyName("product_id")]
    public int? ProductId { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("qu_id")]
    public int? QuantityUnitId { get; set; }

    [JsonPropertyName("ingredient_group")]
    public string IngredientGroup { get; set; }

    [JsonPropertyName("price_factor")]
    public double PriceFactor { get; set; } = 1.0;

    [JsonPropertyName("variable_amount")]
    public string VariableAmount { get; set; }
}

