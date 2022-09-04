using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using Darlek.Core.UI;

namespace Darlek.Core.SchemeLibrary;

[RuntimeType]
public static class MenuMethods
{
    [RuntimeMethod("show-menu")]
    public static object ShowMenu(Menu menu)
    {
        menu.Show();

        return None.Instance;
    }
}
