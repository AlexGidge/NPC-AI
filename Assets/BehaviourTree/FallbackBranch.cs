using System;

namespace BehaviourTree
{
    public abstract class FallbackBranch<T> : Branch<T> where T : TreeContext
    {
        public FallbackBranch(T _context) : base(_context)
        {
        }
        
        public override NodeResult Start()
        {
            return Process();
        }

        public override NodeResult Process()
        {
            foreach (Node<T> child in Children)
            {
                switch (ProcessChild(child).ResultState)
                {
                    case NodeResultState.New:
                        CurrentState = NodeResult.Processing;
                        break;
                    case NodeResultState.Success:
                        CurrentState = NodeResult.Success;
                        return CurrentState;//One child succeeded so return
                    case NodeResultState.Failure:
                        //Do nothing on failure
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