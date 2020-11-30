using System;
using BookGenerator.Core.Engine.Core;

namespace BookGenerator.Core.Engine.Expressions
{
#if !(PORTABLE || NETCORE)

	[Serializable]
#endif
	public sealed class LogicalNegation : Expression
	{
		protected internal override PredictedType ResultType
		{
			get
			{
				return PredictedType.Bool;
			}
		}

		internal override bool ResultInTempContainer
		{
			get { return false; }
		}

		public LogicalNegation(Expression first)
			: base(first, null, false)
		{
		}

		public override JSValue Evaluate(Context context)
		{
			return !(bool)_left.Evaluate(context);
		}

		public override T Visit<T>(Visitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		public override string ToString()
		{
			return "!" + _left;
		}
	}
}