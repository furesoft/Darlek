using Darlek.Core;
using Darlek.Core.RuntimeLibrary;
using Darlek.Core.SchemeLibrary;
using Darlek.Core.Schemy;
using Darlek.Library;
using Spectre.Console;
using System;
using System.IO;

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
            var res = interpreter.Evaluate(new StringReader(input));

            if (res.Error != null)
            {
                AnsiConsole.WriteLine(res.Error.ToString());
            }

            AnsiConsole.WriteLine(res.Result.ToString());
        }
    }
}