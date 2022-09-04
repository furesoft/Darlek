using System;

namespace Darlek.Core;

public class DelegateCommand : IMenuCommand
{
    private Action<Menu> Action;

    public DelegateCommand(Action<Menu> action)
    {
        Action = action;
    }

    public void Invoke(Menu parentMenu)
    {
        Action(parentMenu);
    }
}