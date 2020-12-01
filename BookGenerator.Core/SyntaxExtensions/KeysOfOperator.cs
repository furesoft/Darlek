using BookGenerator.Core.Engine.Core;
using BookGenerator.Core.Engine.Expressions;

namespace BookGenerator.Core.SyntaxExtensions
{
    [CustomCodeFragment(CodeFragmentType.Expression, "keysof")]
    public sealed class KeysOfOperator : Expression
    {
        public KeysOfOperator(Expression source)
            : base(source, null, false)
        {
        }

        public static bool Validate(string code, int position)
        {
            return Parser.Validate(code, "keysof", position);
        }

        public static CodeNode Parse(ParseInfo state, ref int position)
        {
            if (!Parser.Validate(state.Code, "keysof", ref position))
                return null;

            while (char.IsWhiteSpace(state.Code, position))
                position++;

            int start = position;

            var source = ExpressionTree.Parse(state, ref position, true);

            return new KeysOfOperator(source);
        }

        public override JSValue Evaluate(Context context)
        {
            return JSObject.getOwnPropertyNames(new Arguments { LeftOperand.Evaluate(context) });
        }
    }
}