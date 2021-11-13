namespace BehaviourTree
{
    public abstract class Root<T> : Node<T> where T : TreeContext
    {
        protected Root(T _context) : base(_context)
        {
        }
    }
}