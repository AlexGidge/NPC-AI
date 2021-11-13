using System;

namespace BehaviourTree
{
    public abstract class BranchSequence<T> : Branch<T> where T : TreeContext
    {
        public BranchSequence(T _context, BranchType branchType) : base(_context, branchType)
        {
        }

        public override NodeResult Process()
        {
            return ProcessSequence();
        }
        
        private NodeResult ProcessSequence()
        {
            switch (BranchType)
            {
                case BranchType.PopQueue:
                    return RunPopQueue();
                case BranchType.Async:
                    return RunAsyncSequence();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private NodeResult RunAsyncSequence()
        {
            NodeResult result = NodeResult.Success;

            foreach (Node<T> child in Children)
            {
                NodeResult childResult;
                
                if (child.CurrentState.ResultState == NodeResultState.Processing)
                {
                    childResult = child.Process();
                    //else success, continue to next
                }
                else if (child.CurrentState.ResultState == NodeResultState.New)
                {
                    childResult = child.Initialise();
                }
                else
                {
                    continue;
                }
                
                if (childResult.ResultState == NodeResultState.Failure)
                {
                    //TODO: Handle Failures. 
                }
                else if (childResult.ResultState == NodeResultState.Processing)
                {
                    result = NodeResult.Processing;
                }
            }

            CurrentState = result;
            return CurrentState;
        }

        private NodeResult RunPopQueue()
        {
            NodeResult result;
            
            if (CurrentNode == null)
            {
                Node<T> nextNode = Children.Pop();
                if (nextNode != null)
                {
                    CurrentNode = nextNode;
                    result = StartCurrentChild();
                }
                else
                {
                    CurrentState = NodeResult.Success;//Return success when all children have passed
                }
            }
            else
            {
                result = RunCurrentChild();
                if (result.ResultState == NodeResultState.Success)
                {
                    CurrentNode = null;
                    CurrentState = NodeResult.Processing;
                }
            }

            return CurrentState;
        }
    }
}