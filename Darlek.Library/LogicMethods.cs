using Darlek.Core.RuntimeLibrary;

namespace Darlek.Library;

[RuntimeType]
public static class LogicMethods
{
    [RuntimeMethod("^")]
    public static int Xor(int left, int right)
    {
        return left ^ right;
    }

    [RuntimeMethod("<<")]
    public static int LeftShift(int mask, int index)
    {
        return mask << index;
    }

    [RuntimeMethod(">>")]
    public static int RightShift(int mask, int index)
    {
        return mask >> index;
    }

    [RuntimeMethod("negate")]
    public static int Negate(int value)
    {
        return ~value + 1;
    }

    [RuntimeMethod("set-bit")]
    public static int SetBit(int mask, int index)
    {
        return mask | 1 << index;
    }

    [RuntimeMethod("&")]
    public static int And(int left, int right)
    {
        return left & right;
    }

    [RuntimeMethod("|")]
    public static int Or(int left, int right)
    {
        return left | right;
    }

    [RuntimeMethod("~")]
    public static int Not(int left)
    {
        return ~left;
    }
}