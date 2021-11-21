using System;

namespace BehaviourTree
{
    public abstract class SequenceBranch<T> : Branch<T> where T : TreeContext
    {
        public override NodeResult Start()
        {
            return Process();
        }

        public override NodeResult Process()
        { 
            if (CurrentNode == null)
            {
                GetNextNode();
            }
            
            switch (ProcessChild(CurrentNode).ResultState)
            {
                case NodeResultState.Success:
                    //Node complete so move to next
                    CurrentNode = null;
                    CurrentState = NodeResult.Processing;
                    break;
                case NodeResultState.Failure:
                    CurrentState = NodeResult.Failure;
                    return CurrentState;//Break on failure
                case NodeResultState.Processing:
                    CurrentState = NodeResult.Processing;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return CurrentState;
        }
        
        private void GetNextNode()
        {
            Node<T> nextNode = Children.Pop();
            CurrentNode = nextNode;
        }


        public SequenceBranch(T _context) : base(_context)
        {
        }
    }
}