using Darlek.Core.RuntimeLibrary;
using LiteDB;

namespace Darlek.Core.SchemeLibrary;

[RuntimeType()]
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

    [RuntimeMethod("get-all-recipes")]
    public static object GetRecipes()
    {
        return Repository.GetAll<BsonDocument>();
    }
}