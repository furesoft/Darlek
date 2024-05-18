// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Darlek.Core.Schemy;

using System;

public class None
{
    public static readonly None Instance = new None();
}

class AssertionFailedError(string msg) : Exception(msg)
{
}

class SyntaxError(string msg) : Exception(msg)
{
}

/// <summary>
/// Poor man's discreminated union
/// </summary>
public class Union<T1, T2>
{
    private readonly object data;
    public Union(T1 data)
    {
        this.data = data;
    }

    public Union(T2 data)
    {
        this.data = data;
    }

    public TResult Use<TResult>(Func<T1, TResult> func1, Func<T2, TResult> func2)
    {
        if (data is T1)
        {
            return func1((T1)data);
        }
        else
        {
            return func2((T2)data);
        }
    }
}
