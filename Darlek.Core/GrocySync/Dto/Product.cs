namespace Darlek.Core.GrocySync.Dto;

public class Product
{
    public int? id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int? location_id { get; set; }
    public int? qu_id_purchase { get; set; }
    public int? qu_id_stock { get; set; }
    public int? enable_tare_weight_handling { get; set; }
    public int? not_check_stock_fulfillment_for_recipes { get; set; }
    public int? product_group_id { get; set; }
    public double tare_weight { get; set; }
    public double min_stock_amount { get; set; }
    public int? default_best_before_days { get; set; }
    public int? default_best_before_days_after_open { get; set; }
    public string picture_file_name { get; set; }
    public string row_created_timestamp { get; set; }
    public int? shopping_location_id { get; set; }
    public int? treat_opened_as_out_of_stock { get; set; }
    public int? auto_reprint_stock_label { get; set; }
    public int? no_own_stock { get; set; }
    public object userfields { get; set; }
    public int? should_not_be_frozen { get; set; }
    public int? default_consume_location_id { get; set; }
    public int? move_on_open { get; set; }

    public override string ToString()
    {
        return name;
    }
}