using Creek.Parsing.Sprache;
using Furesoft.Core.AST;

namespace BookGenerator.Core
{
    public class SqlParser
    {
        public static Parser<IAstNode> Select =
            Parse.String("select").Select(_ => NodeFactory.Call("select"));
    }
}