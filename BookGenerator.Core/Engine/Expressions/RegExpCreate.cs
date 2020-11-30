using System;
using BookGenerator.Core.Engine.BaseLibrary;
using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Statements;

namespace BookGenerator.Core.Engine.Expressions
{
#if !(PORTABLE || NETCORE)

    [Serializable]
#endif
    public sealed class RegExpExpression : Expression
    {
        private string pattern;
        private string flags;

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

        protected internal override PredictedType ResultType
        {
            get
            {
                return PredictedType.Object;
            }
        }

        public RegExpExpression(string pattern, string flags)
        {
            this.pattern = pattern;
            this.flags = flags;
        }

        public static CodeNode Parse(ParseInfo state, ref int position)
        {
            var i = position;
            if (!Parser.ValidateRegex(state.Code, ref i, false))
                return null;

            var value = state.Code.Substring(position, i - position);
            position = i;

            state.Code = Parser.RemoveComments(state.SourceCode, i);
            var s = value.LastIndexOf('/') + 1;
            var flags = value.Substring(s);
            try
            {
                return new RegExpExpression(value.Substring(1, s - 2), flags); // объекты должны быть каждый раз разные
            }
            catch (Exception e)
            {
                if (state.message != null)
                    state.message(MessageLevel.Error, i - value.Length, value.Length, string.Format(Strings.InvalidRegExp, value));
                return new ExpressionWrapper(new Throw(e));
            }
        }

        protected internal override CodeNode[] GetChildrenImpl()
        {
            return null;
        }

        public override JSValue Evaluate(Context context)
        {
            return new RegExp(pattern, flags);
        }

        public override T Visit<T>(Visitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override string ToString()
        {
            return "/" + pattern + "/" + flags;
        }
    }
}