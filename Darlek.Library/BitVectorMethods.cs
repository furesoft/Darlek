using Darlek.Core.RuntimeLibrary;
using Darlek.Library.Types;

namespace Darlek.Library;

[RuntimeType("bitvector")]
public static class BitVectorMethods
{
    [RuntimeMethod("bitvector-set")]
    public static void Set(BitSet set, int index)
    {
        set.Set(index);
    }

    [RuntimeMethod("bitvector-unset")]
    public static void Unset(BitSet set, int index)
    {
        set.Unset(index);
    }

    [RuntimeMethod("bitvector-set?")]
    public static bool IsSet(BitSet set, int index)
    {
        return set.IsSet(index);
    }
}