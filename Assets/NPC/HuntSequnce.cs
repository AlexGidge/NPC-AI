using System.Collections.Generic;
using BehaviourTree;

public class HuntSequence<T> : SequenceBranch<T> where T : NPCContext
{
    public HuntSequence(T _context) : base(_context)
    {
    }

    public override void PopulateChildren()
    {
        Children = new Stack<Node<T>>();
        
        Children.Push(new ChaseTarget<T>(context));
        Children.Push(new FindTarget<T>(context));//TODO: Idle till collision?
    }
}
