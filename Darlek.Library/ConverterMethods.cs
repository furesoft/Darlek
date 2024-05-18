using Darlek.Core.RuntimeLibrary;
using Darlek.Library.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darlek.Library;

[RuntimeType("conversion")]
public static class ConverterMethods
{
    [RuntimeMethod("binary->string")]
    public static string ToString(byte[] input)
    {
        return Encoding.Default.GetString(input);
    }

    [RuntimeMethod("struct->value-list")]
    public static List<object> ToValueList(RuntimeStruct stru)
    {
        return [.. stru.Values];
    }

    [RuntimeMethod("string->num")]
    public static int ToNum(string input)
    {
        return int.Parse(input);
    }

    [RuntimeMethod("list->base64")]
    public static string ToBase64(List<object> src)
    {
        return Convert.ToBase64String(src.Cast<byte>().ToArray());
    }

    [RuntimeMethod("num->byte")]
    public static byte ToByte(int num)
    {
        return Convert.ToByte(num);
    }

    [RuntimeMethod("base64->list")]
    public static List<byte> ToList(string src)
    {
        return Convert.FromBase64String(src).ToList();
    }

    [RuntimeMethod("num->bitvector")]
    public static BitSet ToBitvector(int num)
    {
        return new BitSet(num);
    }

    [RuntimeMethod("bitvector->num")]
    public static int ToNum(BitSet bv)
    {
        return (int)bv;
    }
}