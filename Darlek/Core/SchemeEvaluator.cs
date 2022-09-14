using Darlek.Commands;
using Darlek.Core.Crawler;
using Darlek.Core.ImportProviders;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library;
using Spectre.Console;
using System;
using System.IO;

namespace Darlek.Core;

public class SchemeEvaluator
{
    public static Interpreter Init()
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

            if (args.Count == 2)
                menu.Items.Add(cmd.Name, cmd);
            else
                ((Menu)args[2]).Items.Add(cmd.Name, cmd);
            return None.Instance;
        }));

        ctx.DefineGlobal(Symbol.FromString("main-menu"), menu);
        ctx.DefineGlobal(Symbol.FromString("manage-menu"), ManageMenu.Menu);

        ctx.DefineGlobal(Symbol.FromString("register-importer"), new NativeProcedure((args) => {
            var invoker = (Procedure)args[1];

            var importer = new SchemeImporter((Symbol)args[0], invoker);
            ImportProvider.Register(importer);

            return None.Instance;
        }));

        ctx.DefineGlobal(Symbol.FromString("register-crawler"), NativeProcedure.Create<string, Procedure, object>((host, callback) => {
            var crawler = new SchemeCrawler(callback);
            CrawlerFactory.Crawlers.Add(host, crawler);

            return callback;
        }));

        var result = ctx.Evaluate(new StringReader(source));
        if (result.Error != null)
        {
            AnsiConsole.MarkupLine($"[red]{result.Error}[/]");
            Console.ReadKey();
        }
    }
}