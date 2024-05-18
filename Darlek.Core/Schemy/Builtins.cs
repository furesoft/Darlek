// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Darlek.Core.Schemy;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Extend the interpreter with essential builtin functionalities
/// </summary>
public class Builtins
{
    public static IDictionary<Symbol, object> CreateBuiltins(Interpreter interpreter)
    {
        var builtins = new Dictionary<Symbol, object>
        {
            [Symbol.FromString("+")] = new NativeProcedure(Utils.MakeVariadic(Add), "+"),
            [Symbol.FromString("-")] = new NativeProcedure(Utils.MakeVariadic(Minus), "-"),
            [Symbol.FromString("*")] = new NativeProcedure(Utils.MakeVariadic(Multiply), "*"),
            [Symbol.FromString("/")] = new NativeProcedure(Utils.MakeVariadic(Divide), "/"),
            [Symbol.FromString("=")] = NativeProcedure.Create<double, double, bool>((x, y) => x == y, "="),
            [Symbol.FromString("<")] = NativeProcedure.Create<double, double, bool>((x, y) => x < y, "<"),
            [Symbol.FromString("<=")] = NativeProcedure.Create<double, double, bool>((x, y) => x <= y, "<="),
            [Symbol.FromString(">")] = NativeProcedure.Create<double, double, bool>((x, y) => x > y, ">"),
            [Symbol.FromString(">=")] = NativeProcedure.Create<double, double, bool>((x, y) => x >= y, ">="),
            [Symbol.FromString("eq?")] = NativeProcedure.Create<object, object, bool>((x, y) => ReferenceEquals(x, y), "eq?"),
            [Symbol.FromString("equal?")] = NativeProcedure.Create<object, object, bool>(EqualImpl, "equal?"),
            [Symbol.FromString("boolean?")] = NativeProcedure.Create<object, bool>(x => x is bool, "boolean?"),
            [Symbol.FromString("num?")] = NativeProcedure.Create<object, bool>(x => x is int || x is double, "num?"),
            [Symbol.FromString("string?")] = NativeProcedure.Create<object, bool>(x => x is string, "string?"),
            [Symbol.FromString("symbol?")] = NativeProcedure.Create<object, bool>(x => x is Symbol, "symbol?"),
            [Symbol.FromString("list?")] = NativeProcedure.Create<object, bool>(x => x is List<object>, "list?"),
            [Symbol.FromString("map")] = NativeProcedure.Create<ICallable, List<object>, List<object>>((func, ls) => ls.Select(x => func.Call([x])).ToList()),
            [Symbol.FromString("reverse")] = NativeProcedure.Create<List<object>, List<object>>(ls => ls.Reverse<object>().ToList()),
            [Symbol.FromString("range")] = new NativeProcedure(RangeImpl, "range"),
            [Symbol.FromString("apply")] = NativeProcedure.Create<ICallable, List<object>, object>((proc, args) => proc.Call(args), "apply"),
            [Symbol.FromString("list")] = new NativeProcedure(args => args, "list"),
            [Symbol.FromString("list-ref")] = NativeProcedure.Create<List<object>, int, object>((ls, idx) => ls[idx]),
            [Symbol.FromString("length")] = NativeProcedure.Create<List<object>, int>(list => list.Count, "length"),
            [Symbol.FromString("car")] = NativeProcedure.Create<List<object>, object>(args => args[0], "car"),
            [Symbol.FromString("cdr")] = NativeProcedure.Create<List<object>, List<object>>(args => args.Skip(1).ToList(), "cdr"),
            [Symbol.CONS] = NativeProcedure.Create<object, List<object>, List<object>>((x, ys) => (new[] { x }).Concat(ys).ToList(), "cons"),
            [Symbol.FromString("not")] = NativeProcedure.Create<bool, bool>(x => !x, "not"),
            [Symbol.APPEND] = NativeProcedure.Create<List<object>, List<object>, List<object>>((l1, l2) => l1.Concat(l2).ToList(), "append"),
            [Symbol.FromString("null")] = NativeProcedure.Create(() => (object)null, "null"),
            [Symbol.FromString("nil")] = NativeProcedure.Create(() => (object)null, "nil"),
            [Symbol.FromString("null?")] = NativeProcedure.Create<object, bool>(x => x is List<object> && ((List<object>)x).Count == 0, "null?"),
            [Symbol.FromString("nil?")] = NativeProcedure.Create<object, bool>(x => x is List<object> && ((List<object>)x).Count == 0, "nil?"),
            [Symbol.FromString("assert")] = new NativeProcedure(AssertImpl, "assert"),
            [Symbol.FromString("load")] = NativeProcedure.Create<string, None>(filename => LoadImpl(interpreter, filename), "load")
        };

        return builtins;
    }

    #region Builtin Implementations

    private static List<object> RangeImpl(List<object> args)
    {
        Utils.CheckSyntax(args, args.Count >= 1 && args.Count <= 3);
        foreach (var item in args)
        {
            Utils.CheckSyntax(args, item is int, "items must be integers");
        }

        int start, end, step;
        if (args.Count == 1)
        {
            start = 0;
            end = (int)args[0];
            step = 1;
        }
        else if (args.Count == 2)
        {
            start = (int)args[0];
            end = (int)args[1];
            step = 1;
        }
        else
        {
            start = (int)args[0];
            end = (int)args[1];
            step = (int)args[2];
        }

        if (start < end) Utils.CheckSyntax(args, step > 0, "step must make the sequence end");
        if (start > end) Utils.CheckSyntax(args, step < 0, "step must make the sequence end");

        var res = new List<object>();

        if (start <= end) for (var i = start; i < end; i += step) res.Add(i);
        else for (var i = start; i > end; i += step) res.Add(i);

        res.TrimExcess();
        return res;
    }

    private static None AssertImpl(List<object> args)
    {
        Utils.CheckArity(args, 1, 2);
        var msg = "Assertion failed";
        msg += args.Count > 1 ? ": " + Utils.ConvertType<string>(args[1]) : string.Empty;
        var pred = Utils.ConvertType<bool>(args[0]);
        if (!pred) throw new AssertionFailedError(msg);
        return None.Instance;
    }

    private static None LoadImpl(Interpreter interpreter, string filename)
    {
        using (TextReader reader = new StreamReader(interpreter.FileSystemAccessor.OpenRead(filename)))
        {
            interpreter.Evaluate(reader);
        }

        return None.Instance;
    }

    public static bool EqualImpl(object x, object y)
    {
        if (Equals(x, y)) return true;
        if (x == null || y == null) return false;

        if (x is IList<object> && y is IList<object>)
        {
            var x2 = (IList<object>)x;
            var y2 = (IList<object>)y;
            if (x2.Count != y2.Count) return false;
            return x2.Zip(y2, (a, b) => Tuple.Create(a, b))
                             .All(pair => EqualImpl(pair.Item1, pair.Item2));
        }

        return false;
    }

    private static object Add(object x, object y)
    {
        if (x is int && y is int) return (int)x + (int)y;
        if (x is string && y is Symbol) return Symbol.FromString(x + ((Symbol)y).AsString);
        if (x is Symbol && y is string) return Symbol.FromString(((Symbol)x).AsString + y);
        return (double)Convert.ChangeType(x, typeof(double)) + (double)Convert.ChangeType(y, typeof(double));
    }

    private static object Minus(object x, object y)
    {
        if (x is int && y is int) return (int)x - (int)y;
        return (double)Convert.ChangeType(x, typeof(double)) - (double)Convert.ChangeType(y, typeof(double));
    }

    private static object Multiply(object x, object y)
    {
        if (x is int && y is int) return (int)x * (int)y;
        return (double)Convert.ChangeType(x, typeof(double)) * (double)Convert.ChangeType(y, typeof(double));
    }

    private static object Divide(object x, object y)
    {
        if (x is int && y is int) return (int)x / (int)y;
        return (double)Convert.ChangeType(x, typeof(double)) / (double)Convert.ChangeType(y, typeof(double));
    }

    #endregion Builtin Implementations
}