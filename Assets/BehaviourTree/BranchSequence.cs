using System;

namespace BehaviourTree
{
    public abstract class BranchSequence<T> : Branch<T> where T : TreeContext
    {
        protected readonly bool BreakOnFailure;
        
        public BranchSequence(T _context, BranchType branchType, bool breakOnFailure) : base(_context, branchType)
        {
            BreakOnFailure = breakOnFailure;
        }

        public override NodeResult Initialise()
        {
            return Process();
        }

        public override NodeResult Process()
        {
            switch (BranchType)
            {
                case BranchType.Await:
                    return RunChildrenSync();
                case BranchType.Async:
                    return RunChildrenAsync();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Runs 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private NodeResult RunChildrenAsync()
        {
            CurrentState = NodeResult.Processing;
            
            foreach (Node<T> child in Children)
            {
                NodeResult childResult = HandleChild(child);
                switch (childResult.ResultState)
                {
                    case NodeResultState.New:
                        CurrentState = NodeResult.Processing;
                        break;
                    case NodeResultState.Success:
                        //No change
                        break;
                    case NodeResultState.Failure:
                        CurrentState = NodeResult.Failure;
                        if (BreakOnFailure)
                        {
                            return CurrentState;
                        }
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

        private NodeResult HandleChild(Node<T> child) 
        {
            if (child != null)
            {
                NodeResult childResult;
                switch (child.CurrentState.ResultState)
                {
                    case NodeResultState.New:
                        childResult = child.Initialise();
                        break;
                    case NodeResultState.Success:
                        childResult = NodeResult.Success;
                        break;
                    case NodeResultState.Failure:
                        childResult = NodeResult.Failure;
                        break;
                    case NodeResultState.Processing:
                        childResult = child.Process();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return childResult;
            }
            return NodeResult.Success;
        }

        private NodeResult RunChildrenSync()
        {
            if (CurrentNode == null)
            {
                GetNextNode();
            }
            
            switch (HandleChild(CurrentNode).ResultState)
            {
                case NodeResultState.New://TODO: New vs processing
                    CurrentState = NodeResult.Processing;
                    break;
                case NodeResultState.Success:
                    //Node complete so move to next
                    CurrentNode = null;
                    CurrentState = new NodeResult();
                    break;
                case NodeResultState.Failure:
                    if (BreakOnFailure)
                    {
                        CurrentState = NodeResult.Failure;
                        return CurrentState;
                    }
                    else
                    {
                        GetNextNode();
                    }
                    break;
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
    }
}