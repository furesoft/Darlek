using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Library;
using Darlek.Scheme;
using Spectre.Console;
using System;
using System.IO;

namespace Darlek.Commands;

internal class SchemeReplCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var interpreter = new Interpreter();

        SchemeCliLoader.InitGlobals(interpreter);
        SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
        SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

        bool shouldLeave = false;
        interpreter.DefineGlobal(Symbol.FromString("exit-repl"), NativeProcedure.Create<object>(() => { shouldLeave = true; return None.Instance; }));

        while (!shouldLeave)
        {
            var input = Console.ReadLine();
            var res = interpreter.Evaluate(new StringReader(input));

            if (res.Error != null)
            {
                AnsiConsole.WriteLine(res.Error.ToString());
            }
            else
            {
                AnsiConsole.WriteLine(res.Result.ToString());
            }
        }

        parentMenu.Show();
    }
}