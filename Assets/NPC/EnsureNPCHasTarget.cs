using System.Collections.Generic;
using BehaviourTree;

public class EnsureNPCHasTarget<T> : BranchSequence<T> where T : NPCContext
{
    public EnsureNPCHasTarget(T _context) : base(_context, BranchType.Sequence)
    {
    }

    public override void PopulateChildren()
    {
        Children = new Stack<Node<T>>();
        Children.Push(new LookAtTarget<T>(context));
        Children.Push(new FindTarget<T>(context));
    }

    public override NodeResult Initialise()
    {
        throw new System.NotImplementedException();
    }
}
