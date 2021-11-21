using BehaviourTree;

public abstract class Condition<T> : Node<T> where T : TreeContext
{
    public Condition(T _context) : base(_context)
    {
    }

    public override NodeResult Start()
    {
        throw new System.NotImplementedException();
    }
}