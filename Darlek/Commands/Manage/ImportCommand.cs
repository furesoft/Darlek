using Darlek.Core;
using Spectre.Console;
using System.IO;

namespace Darlek.Commands.Manage;

public class ImportCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        string filename = AnsiConsole.Prompt(new TextPrompt<string>("Filename:"));

        var provider = ImportProvider.GetProvider(filename);

        var content = File.ReadAllBytes(filename);
        var document = provider.Import(content);

        Repository.Add(document);

        AnsiConsole.WriteLine("Successfully imported");

        parentMenu.WaitAndShow();
    }
}