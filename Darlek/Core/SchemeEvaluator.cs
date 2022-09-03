using Darlek.Commands;
using Darlek.Core.Crawler;
using Darlek.Core.ImportProviders;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Core.Schemy;
using Darlek.Core.UI;
using Darlek.Library;
using System.IO;

namespace Darlek.Core;

public class SchemeEvaluator
{
    public Interpreter Init()
    {
        var interpreter = new Interpreter();

        SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
        SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

        return interpreter;
    }

    public void AddCustomCommands(string source, Menu menu)
    {
        var ctx = Init();

        ctx.DefineGlobal(Symbol.FromString("register-command"), new NativeProcedure((args) => {
            var invoker = (Procedure)args[1];

            var cmd = new SchemeCommand(args[0].ToString(), invoker);
            menu.Items.Add(cmd.Name, cmd);
            return None.Instance;
        }));

        ctx.DefineGlobal(Symbol.FromString("current-menu"), menu);
        ctx.DefineGlobal(Symbol.FromString("show-menu"), new NativeProcedure((args) => {
            var m = (Menu)args[0];
            m.Show();

            return None.Instance;
        }));

        ctx.DefineGlobal(Symbol.FromString("register-importer"), new NativeProcedure((args) => {
            var invoker = (Procedure)args[1];

            var importer = new SchemeImporter((Symbol)args[0], invoker);
            ImportProvider.Register(importer);

            return None.Instance;
        }));

        var result = ctx.Evaluate(new StringReader(source));
    }

    public ICrawler GetCrawler(string source)
    {
        return new SchemeCrawler(source);
    }
}