using System.Collections.Generic;

namespace JWOAGameSystem
{
    public abstract class Composer : Node
    {
        public Composer() : base() { }
        public Composer(List<Node> children) : base(children) { }
    }
}