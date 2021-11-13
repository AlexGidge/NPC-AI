using System.Collections.Generic;
using BehaviourTree;

public class HuntSequence<T> : BranchSequence<T> where T : NPCContext
{
    public HuntSequence(T _context) : base(_context, BranchType.PopQueue)
    {
    }

    public override void PopulateChildren()
    {
        Children = new Stack<Node<T>>();
        
        Children.Push(new ChaseTarget<T>(context));//TODO: FIFO
        Children.Push(new FindTarget<T>(context));//TODO: FIFO
    }

    public override NodeResult Initialise()
    {
        return NodeResult.Processing;
    }
}
