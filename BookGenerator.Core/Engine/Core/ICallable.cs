using BookGenerator.Core.Engine.BaseLibrary;

namespace BookGenerator.Core.Engine.Core
{
    public interface ICallable
    {
        FunctionKind Kind { get; }

        JSValue Construct(Arguments arguments);

        JSValue Construct(JSValue targetObject, Arguments arguments);

        JSValue Call(JSValue targetObject, Arguments arguments);
    }
}