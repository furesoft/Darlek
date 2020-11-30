﻿using System.Collections.Generic;
using BookGenerator.Core.Engine.Core;

namespace BookGenerator.Core.Engine.Expressions
{
	public sealed class ExpressionWrapper : Expression
	{
		private CodeNode node;

		public CodeNode Node { get { return node; } }

		protected internal override bool ContextIndependent
		{
			get
			{
				return false;
			}
		}

		internal override bool ResultInTempContainer
		{
			get { return false; }
		}

		public ExpressionWrapper(CodeNode node)
		{
			this.node = node;
		}

		public override JSValue Evaluate(Context context)
		{
			return node.Evaluate(context);
		}

		public override T Visit<T>(Visitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		public override bool Build(ref CodeNode _this, int expressionDepth, Dictionary<string, VariableDescriptor> variables, CodeContext codeContext, InternalCompilerMessageCallback message, FunctionInfo stats, Options opts)
		{
			_codeContext = codeContext;

			return node.Build(ref node, expressionDepth, variables, codeContext | CodeContext.InExpression, message, stats, opts);
		}
	}
}