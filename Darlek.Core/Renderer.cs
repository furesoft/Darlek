using LiteDB;
using Scriban;
using Scriban.Runtime;
using System.Collections.Generic;

namespace Darlek.Core;

public static class Renderer
{
    public static string RenderObject(ScriptObject sobj, string content)
    {
        var context = new TemplateContext();
        context.PushGlobal(sobj);
        context.EnableRelaxedMemberAccess = true;

        var template = Template.Parse(content);
        var result = template.Render(context);

        return result;
    }

    public static ScriptObject BuildScriptObject(BsonValue obj)
    {
        var sobj = new ScriptObject();

        foreach (var kv in obj.AsDocument)
        {
            var renamedKey = StandardMemberRenamer.Rename(kv.Key);

            if (kv.Value is BsonDocument value)
            {
                sobj.Add(renamedKey, BuildScriptObject(value));
            }
            else if (kv.Value is BsonArray arr)
            {
                var items = new List<object>();

                foreach (var arrItem in arr)
                {
                    items.Add(BuildScriptObject(arrItem));
                }

                sobj.Add(renamedKey, items);
            }
            else
            {
                sobj.Add(renamedKey, kv.Value.RawValue);
            }
        }

        return sobj;
    }
}