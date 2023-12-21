using System.Collections.Generic;

namespace JWOAGameSystem
{
    public abstract class Condition : Node
    {
        public Condition() : base() { }
        public Condition(List<Node> children) : base(children) { }
    }
}