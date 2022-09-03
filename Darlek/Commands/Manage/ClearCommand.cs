using Darlek.Core;
using Darlek.Core.UI;

namespace Darlek.Commands;

public class ClearCommand : IMenuCommand
{
    public void Invoke(Menu parentMenu)
    {
        Repository.Clear();
    }
}