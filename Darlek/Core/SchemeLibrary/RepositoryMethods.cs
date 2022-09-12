using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using LiteDB;

namespace Darlek.Core.SchemeLibrary;

[RuntimeType]
public static class RepositoryMethods
{
    [RuntimeMethod("repository-get-metadata")]
    public static object GetMetadata(string key)
    {
        return Repository.GetMetadata(key);
    }

    [RuntimeMethod("get-recipe")]
    public static object GetRecipe(string id)
    {
        return Repository.Get(id);
    }

    [RuntimeMethod("get-recipes")]
    public static object GetRecipes()
    {
        return Repository.GetAll<BsonDocument>();
    }

    [RuntimeMethod("get-recipe-by-id")]
    public static object GetRecipeById(string id)
    {
        return Repository.Get(id);
    }

    [RuntimeMethod("get-recipe-by-name")]
    public static object GetRecipeByName(string name)
    {
        return Repository.GetByName(name);
    }

    [RuntimeMethod("add-recipe")]
    public static object AddRecipe(string url)
    {
        Repository.Crawl(url);

        return None.Instance;
    }

    [RuntimeMethod("update-recipe")]
    public static object UpdateRecipe(BsonDocument recipe)
    {
        Repository.Update(recipe);

        return None.Instance;
    }

    [RuntimeMethod("remove-recipe")]
    public static object RemoveRecipe(BsonDocument recipe)
    {
        Repository.Remove(recipe);

        return None.Instance;
    }
}