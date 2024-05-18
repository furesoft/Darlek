using Darlek.Core;
using Darlek.Core.Schemy;

namespace Darlek.Commands;

public class SchemeCommand(string name, Procedure invoker) : IMenuCommand
{
    public Procedure Invoker = invoker;
    private readonly string _name = name;

    public string Name => _name;

    public void Invoke(Menu parentMenu)
    {
        Invoker.Call([parentMenu]);
    }
}