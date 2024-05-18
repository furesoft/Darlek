using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library;
using System;
using System.IO;
using Spectre.Console;

namespace Darlek.Commands;

internal class SchemeReplCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var interpreter = new Interpreter();

        SchemeCliLoader.Apply(typeof(StringMethods).Assembly, interpreter);
        SchemeCliLoader.Apply(typeof(RepositoryMethods).Assembly, interpreter);

        while (true)
        {
            var input = Console.ReadLine();

            if (input == ":q") {
                parentMenu.Show();

                return;
            }

            var res = interpreter.Evaluate(new StringReader(input));

            if (res.Error != null)
                AnsiConsole.WriteLine(res.Error.Message);
            else
                AnsiConsole.WriteLine(res.Result.ToString());
        }
    }
}