using Darlek.Core;
using Spectre.Console;
using System.IO;
using System.Linq;

namespace Darlek.Commands;

public class OpenCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        // required elements: title, path, author
        var books = Directory.GetFiles(Repository.BaseDir, "*.darlek").Select(_ => Path.GetFileNameWithoutExtension(_));

        var booksSelector = new Menu(parentMenu);
        foreach (var book in books)
        {
            booksSelector.Items.Add(book, null);
        }

        var selected = booksSelector.Show();

        if (selected == string.Empty) return;

        Repository.CacheFile = Path.Combine(Repository.BaseDir, selected + ".darlek");

        ManageMenu.Show();
    }
}