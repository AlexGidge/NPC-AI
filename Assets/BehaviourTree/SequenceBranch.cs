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

            if (CurrentNode != null)
            {
                switch (ProcessChild(CurrentNode).ResultState)
                {
                    case NodeResultState.New:
                        CurrentState = NodeResult.Processing;
                        break;
                    case NodeResultState.Success:
                        //Node complete so move to next
                        CurrentNode = null;
                        CurrentState = NodeResult.Success;
                        break;
                    case NodeResultState.Failure:
                        CurrentState = NodeResult.Failure;
                        return CurrentState; //Break on failure
                    case NodeResultState.Processing:
                        CurrentState = NodeResult.Processing;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                CurrentState = NodeResult.Failure;//TODO: Throw exception?
            }

            return CurrentState;
        }
        
        private void GetNextNode()
        {
            if (Children.Count > 0)
            {
                Node<T> nextNode = Children.Pop();
                CurrentNode = nextNode;
            }
            else
            {
                CurrentNode = null;
            }
        }


        public SequenceBranch(T _context) : base(_context)
        {
        }
    }
}