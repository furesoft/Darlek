using Darlek.Core;

namespace Darlek.Commands.Manage;

public class ClearCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        Repository.Clear();

        parentMenu.WaitAndShow();
    }
}