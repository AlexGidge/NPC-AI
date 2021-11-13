using System.Collections.Generic;
using BehaviourTree;

public class ChaseTarget<T> : BranchSequence<T> where T : NPCContext
    {
        public ChaseTarget(T _context) : base(_context, BranchType.Async, true)
        {
        }

        public override void PopulateChildren()
        {
            Children = new Stack<Node<T>>();
            
            Children.Push(new MoveToTarget<T>(context));
            Children.Push(new LookAtTarget<T>(context));
        }
    }