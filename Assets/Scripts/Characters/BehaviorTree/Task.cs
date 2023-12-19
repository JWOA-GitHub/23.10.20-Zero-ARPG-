using System.Collections.Generic;

namespace JWOAGameSystem
{
    public abstract class Task : Node
    {
        public Task() : base() { }
        public Task(List<Node> children) : base(children) { }
    }
}