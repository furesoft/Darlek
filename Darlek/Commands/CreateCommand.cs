using Darlek.Core;
using Darlek.Core.UI;
using Spectre.Console;
using System.IO;

namespace Darlek.Commands;

public class CreateCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var title = AnsiConsole.Prompt(new TextPrompt<string>("Title:"));
        var author = AnsiConsole.Prompt(new TextPrompt<string>("Author:"));

        Repository.CacheFile = Path.Combine(Repository.BaseDir, title + ".darlek");
        Repository.SetMetadata("title", title);
        Repository.SetMetadata("author", author);
        Repository.SetMetadata("filename", "exported");
    }
}