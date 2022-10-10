using Darlek.Core.RuntimeLibrary;
using Darlek.Core.Schemy;
using System.Collections.Generic;

namespace Darlek.Library;

[RuntimeType]
public static class StackMethods
{
    [RuntimeMethod("make-stack")]
    public static Stack<object> Make()
    {
        return new Stack<object>();
    }

    [RuntimeMethod("stack-push!")]
    public static object Push(Stack<object> stack, object value)
    {
        stack.Push(value);

        return None.Instance;
    }

    [RuntimeMethod("stack-pop!")]
    public static object Pop(Stack<object> stack)
    {
        return stack.Pop();
    }

    [RuntimeMethod("stack?")]
    public static bool Pred(object stack)
    {
        return stack is Stack<object>;
    }

    [RuntimeMethod("stack-empty?")]
    public static bool IsEmpty(Stack<object> stack)
    {
        return stack.Count == 0;
    }

    [RuntimeMethod("stack-length")]
    public static int Length(Stack<object> stack)
    {
        return stack.Count;
    }

    [RuntimeMethod("stack-top")]
    public static object Top(Stack<object> stack)
    {
        return stack.Peek();
    }
}