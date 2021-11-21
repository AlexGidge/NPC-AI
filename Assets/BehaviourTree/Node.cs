namespace BehaviourTree
{
    /// <summary>
    /// Base class for all behaviour tree nodes
    /// </summary>
    /// <typeparam name="T">Context for the node, passed through the Node's constructor</typeparam>
    public abstract class Node<T> : INode<T> where T : TreeContext
    {
        public NodeResult CurrentState;
        
        public T context { get; private set; }
        
        public Node(T _context)
        {
            SetContext(_context);
            CurrentState = NodeResult.Processing;
        }
        
        public void SetContext(T _context)
        {
            context = _context;
        }

        public abstract NodeResult Start();//TODO: Rename initialise? Run?
        public abstract NodeResult Process();
    }

    public interface INode<in T> where T : TreeContext
    {
        void SetContext(T context);
    }
}