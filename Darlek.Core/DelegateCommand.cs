using System;

namespace Darlek.Core;

public class DelegateCommand(Action<Menu> action) : IMenuCommand
{
    private Action<Menu> Action = action;

    public void Invoke(Menu parentMenu)
    {
        Action(parentMenu);
    }
}