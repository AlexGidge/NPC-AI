using System.Collections.Generic;
using BehaviourTree;
    public class FindPurpose<T> : FallbackBranch<T> where T : NPCContext
    {
        public FindPurpose(T _context) : base(_context)
        {
        }

        public override NodeResult Start()
        {
            return Process();
        }

        public override void PopulateChildren()
        {
            Children = new Stack<Node<T>>();
        
            Children.Push(new Idle<T>(context));
            //TODO: Collision should interrupt. Maybe interrupt Idle only?
            Children.Push(new HuntSequence<T>(context));
        }
    }