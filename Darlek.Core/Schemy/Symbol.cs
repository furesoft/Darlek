// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Darlek.Core.Schemy;

using System;
using System.Collections.Generic;

/// <summary>
/// Scheme symbol
/// </summary>
/// <remarks>
/// Symbols are interned so that symbols with the same name are actually of the same symbol object instance.
/// </remarks>
public class Symbol : IEquatable<Symbol>
{
    private static readonly IDictionary<string, Symbol> table = new Dictionary<string, Symbol>();
    public static readonly IReadOnlyDictionary<string, Symbol> QuotesMap = new Dictionary<string, Symbol>()
        {
            { "'", QUOTE },
            { "`", QUASIQUOTE},
            { ",", UNQUOTE},
            { ",@", UNQUOTE_SPLICING},
        };

    private readonly string symbol;

    /// <summary>
    /// Initializes a new instance of the <see cref="Symbol"/> class.
    /// </summary>
    /// <param name="sym">The symbol</param>
    /// <remarks>
    /// This is private and the users should call <see cref="FromString"/> to instantiate a symbol object.
    /// </remarks>
    private Symbol(string sym)
    {
        symbol = sym;
    }

    public string AsString {
        get { return symbol; }
    }

    /// <summary>
    /// Returns the interned symbol
    /// </summary>
    /// <param name="sym">The symbol name</param>
    /// <returns>the symbol instance</returns>
    public static Symbol FromString(string sym)
    {
        Symbol res;
        if (!table.TryGetValue(sym, out res))
        {
            table[sym] = new Symbol(sym);
        }

        return table[sym];
    }

    #region wellknown symbols
    public static Symbol IF { get { return FromString("if"); } }
    public static Symbol QUOTE { get { return FromString("quote"); } }
    public static Symbol SET { get { return FromString("set!"); } }
    public static Symbol DEFINE { get { return FromString("define"); } }
    public static Symbol LAMBDA { get { return FromString("lambda"); } }
    public static Symbol BEGIN { get { return FromString("begin"); } }
    public static Symbol DEFINE_MACRO { get { return FromString("define-macro"); } }
    public static Symbol QUASIQUOTE { get { return FromString("quasiquote"); } }
    public static Symbol UNQUOTE { get { return FromString("unquote"); } }
    public static Symbol UNQUOTE_SPLICING { get { return FromString("unquote-splicing"); } }
    public static Symbol EOF { get { return FromString("#<eof-object>"); } }
    public static Symbol APPEND { get { return FromString("append"); } }
    public static Symbol CONS { get { return FromString("cons"); } }
    #endregion wellknown symbols

    #region object implementations
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj is Symbol)
        {
            return Equals(symbol, ((Symbol)obj).symbol);
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        return string.Format("'{0}", symbol);
    }

    public override int GetHashCode()
    {
        return symbol.GetHashCode();
    }

    public bool Equals(Symbol other)
    {
        return ((object)this).Equals(other);
    }
    #endregion object implementations
}
