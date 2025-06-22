namespace Darlek.Core.GrocySync.Extraction;

internal interface IIngredientExtractor
{
    bool Match(string product, string measure);
}