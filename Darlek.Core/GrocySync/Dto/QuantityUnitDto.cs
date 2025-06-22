using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Darlek.Core.GrocySync.Dto;

internal class QuantityUnit
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("name_plural")]
    public string NamePlural { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("row_created_timestamp")]
    public string RowCreatedTimestamp { get; set; }

    [JsonPropertyName("plural_forms")]
    public string PluralForms { get; set; }

    [JsonPropertyName("active")]
    public int Active { get; set; }

    [JsonPropertyName("userfields")]
    public Dictionary<string, string> Userfields { get; set; }
}
