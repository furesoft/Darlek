using Darlek.Commands.Manage;
using Darlek.Core;

namespace Darlek.Commands;

public class SelectCrawlerCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var selected = Utils.Selection(parentMenu, new[] { "chefkoch", "sample" });

        Repository.SetMetadata("crawler", selected);

        parentMenu.WaitAndShow();
    }
}