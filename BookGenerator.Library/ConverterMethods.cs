﻿using BookGenerator.Core.RuntimeLibrary;
using Furesoft.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookGenerator.Library
{
    [RuntimeType]
    public static class ConverterMethods
    {
        [RuntimeMethod("object->string")]
        public static string ToString(object input)
        {
            return input.ToString();
        }

        [RuntimeMethod("string->num")]
        public static int ToNum(string input)
        {
            return int.Parse(input);
        }

        [RuntimeMethod("list->base64")]
        public static string ToBase64(IEnumerable<byte> src)
        {
            return Convert.ToBase64String(src.ToArray());
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
        public static BitSet ToNum(BitSet bv)
        {
            return (int)bv;
        }
    }
}