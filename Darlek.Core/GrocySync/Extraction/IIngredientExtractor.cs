namespace Darlek.Core.GrocySync.Extraction;

public interface IIngredientExtractor
{
    bool Match(string product, string measure);
}