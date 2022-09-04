using Darlek.Core;
using Darlek.Core.Schemy;

namespace Darlek.Commands;

public class SchemeCommand : IMenuCommand
{
    public Procedure Invoker;
    private readonly string _name;

    public SchemeCommand(string name, Procedure invoker)
    {
        _name = name;

        Invoker = invoker;
    }

    public string Name => _name;

    public void Invoke(Menu parentMenu)
    {
        Invoker.Call(new System.Collections.Generic.List<object> { parentMenu });
    }
}