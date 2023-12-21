using System.Collections.Generic;

namespace JWOAGameSystem
{
    public abstract class Action : Node
    {
        public Action() : base() { }
        public Action(List<Node> children) : base(children) { }
    }
}