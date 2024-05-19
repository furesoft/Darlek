using Darlek.Core;

namespace Darlek.Commands.Manage;

public class SearchCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        var list = new Menu(parentMenu);

        list.Items.Add("By Tag", null);

        list.Show();
    }

}
