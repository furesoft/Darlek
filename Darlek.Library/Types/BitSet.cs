﻿using Darlek.Core.RuntimeLibrary;
using System;

namespace Darlek.Library.Types;

/// <summary>
/// A Class To Easily Use Int as BitField
/// </summary>
[RuntimeCtorMethod("bitvector")]
[RuntimeType("bitvector")]
public struct BitSet(int value)
{
    private int flags = value;

    public bool this[int index] {
        get {
            return IsSet(index);
        }
        set {
            if (value)
                Set(index);
            else
                Unset(index);
        }
    }

    public static implicit operator int(BitSet bs)
    {
        return bs.flags;
    }

    public static implicit operator BitSet(int value)
    {
        return new BitSet(value);
    }

    public void SetAll(bool value)
    {
        if (value)
        {
            flags = int.MaxValue;
        }
        else
        {
            flags = 0;
        }
    }

    public void Set(int index)
    {
        flags |= 1 << index;
    }

    public void Unset(int index)
    {
        flags |= 0 << index;
    }

    public bool IsSet(int index)
    {
        return (flags & 1 << index % 32) != 0;
    }

    public override string ToString()
    {
        return Convert.ToString(flags, 2);
    }
}