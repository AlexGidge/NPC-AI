using System;

namespace BehaviourTree
{
    public abstract class Node<T> : INode<T> where T : TreeContext
    {
        public NodeResult CurrentState;
        
        public T context { get; private set; }
        
        public Node(T _context)
        {
            SetContext(_context);
            CurrentState = new NodeResult();
        }
        
        public void SetContext(T _context)
        {
            context = _context;
        }

        public abstract NodeResult Initialise();//TODO: Rename initialise? Run?
        public abstract NodeResult Process();
    }

    public interface INode<in T> where T : TreeContext
    {
        void SetContext(T context);
    }
}