namespace Darlek.Core.GrocySync.Dto;

internal class Product
{
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public int? Id { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string Name { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("description")]
    public string Description { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("location_id")]
    public int? LocationId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("qu_id_purchase")]
    public int? QuantityUnitPurchaseId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("qu_id_stock")]
    public int? QuantityUnitStockId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("enable_tare_weight_handling")]
    public int? EnableTareWeightHandling { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("not_check_stock_fulfillment_for_recipes")]
    public int? NotCheckStockFulfillmentForRecipes { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("product_group_id")]
    public int? ProductGroupId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("tare_weight")]
    public double TareWeight { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("min_stock_amount")]
    public double MinStockAmount { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("default_best_before_days")]
    public int? DefaultBestBeforeDays { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("default_best_before_days_after_open")]
    public int? DefaultBestBeforeDaysAfterOpen { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("picture_file_name")]
    public string PictureFileName { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("row_created_timestamp")]
    public string RowCreatedTimestamp { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("shopping_location_id")]
    public int? ShoppingLocationId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("treat_opened_as_out_of_stock")]
    public int? TreatOpenedAsOutOfStock { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("auto_reprint_stock_label")]
    public int? AutoReprintStockLabel { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("no_own_stock")]
    public int? NoOwnStock { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("userfields")]
    public object Userfields { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("should_not_be_frozen")]
    public int? ShouldNotBeFrozen { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("default_consume_location_id")]
    public int? DefaultConsumeLocationId { get; set; }
    [System.Text.Json.Serialization.JsonPropertyName("move_on_open")]
    public int? MoveOnOpen { get; set; }

    public override string ToString()
    {
        return Name;
    }
}