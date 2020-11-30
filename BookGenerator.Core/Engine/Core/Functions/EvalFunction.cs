using System.Collections.Generic;
using BookGenerator.Core.Engine.BaseLibrary;
using BookGenerator.Core.Engine.Core.Interop;
using BookGenerator.Core.Engine.Expressions;

namespace BookGenerator.Core.Engine.Core.Functions
{
	internal sealed class EvalFunction : Function
	{
		[Hidden]
		public override string name
		{
			[Hidden]
			get
			{
				return "eval";
			}
		}

		[Field]
		[DoNotDelete]
		[DoNotEnumerate]
		[NotConfigurable]
		public override JSValue prototype
		{
			[Hidden]
			get
			{
				return null;
			}
			[Hidden]
			set
			{
			}
		}

		[Hidden]
		public EvalFunction()
		{
			_length = new Number(1);
			_prototype = undefined;
			RequireNewKeywordLevel = RequireNewKeywordLevel.WithoutNewOnly;
		}

		internal override JSValue InternalInvoke(JSValue targetObject, Expression[] arguments, Context initiator, bool withSpread, bool construct)
		{
			if (construct)
				ExceptionHelper.ThrowTypeError("eval can not be called as constructor");

			if (arguments == null || arguments.Length == 0)
				return NotExists;

			return base.InternalInvoke(targetObject, arguments, initiator, withSpread, construct);
		}

		protected internal override JSValue Invoke(bool construct, JSValue targetObject, Arguments arguments)
		{
			if (arguments == null)
				return NotExists;

			var arg = arguments[0];
			if (arg._valueType != JSValueType.String)
				return arg;

			var stack = new Stack<Context>();
			try
			{
				var ccontext = Context.CurrentContext;
				var root = ccontext.RootContext;
				while (ccontext != root && ccontext != null)
				{
					stack.Push(ccontext);
					ccontext = ccontext.Deactivate();
				}
				if (ccontext == null)
				{
					root.Activate();
					try
					{
						return root.Eval(arguments[0].ToString(), false);
					}
					finally
					{
						root.Deactivate();
					}
				}
				else
				{
					return ccontext.Eval(arguments[0].ToString(), false);
				}
			}
			finally
			{
				while (stack.Count != 0)
					stack.Pop().Activate();
			}
		}

		[Hidden]
		public override string ToString(bool headerOnly)
		{
			var result = "function eval()";

			if (!headerOnly)
			{
				result += " { [native code] }";
			}

			return result;
		}
	}
}