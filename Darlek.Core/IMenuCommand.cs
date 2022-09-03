using Darlek.Core.UI;

namespace Darlek.Commands;

public interface IMenuCommand
{
    void Invoke(Menu parentMenu);
}