using System;

namespace BehaviourTree
{
    public abstract class Leaf<T> : Node<T> where T : TreeContext
    {
        protected Leaf(T _context) : base(_context)
        {
        }
    }
}