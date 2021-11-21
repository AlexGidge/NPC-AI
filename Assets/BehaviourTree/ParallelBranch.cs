using System;

namespace BehaviourTree
{
    public abstract class ParallelBranch<T> : Branch<T> where T : TreeContext
    {
        //TODO: New thread for ticks to continue processing on each game tick?
        
        public ParallelBranch(T _context) : base(_context)
        {
        }

        public override NodeResult Start()
        {
            return Process();
        }

        public override NodeResult Process()
        {
            CurrentState = NodeResult.Processing;
            
            foreach (Node<T> child in Children)
            {
                NodeResult childResult = ProcessChild(child);
                switch (childResult.ResultState)
                {
                    case NodeResultState.Success:
                        //No change
                        break;
                    case NodeResultState.Failure:
                        CurrentState = NodeResult.Failure;
                        break;
                    case NodeResultState.Processing:
                        CurrentState = NodeResult.Processing;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return CurrentState;
        }
    }
}