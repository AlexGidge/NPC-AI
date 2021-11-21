using System.Collections.Generic;
using BehaviourTree;

public class ChaseTarget<T> : ParallelBranch<T> where T : NPCContext
    {
        public ChaseTarget(T _context) : base(_context)
        {
        }

        public override void PopulateChildren()
        {
            Children = new Stack<Node<T>>();
            
            Children.Push(new MoveToTarget<T>(context));
            Children.Push(new LookAtTarget<T>(context));
        }
    }