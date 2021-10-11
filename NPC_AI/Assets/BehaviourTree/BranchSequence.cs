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
                    return NodeResult.Success;//Return success when all children have passed
                }
            }
            else
            {
                result = RunCurrentChild();
                if (result.ResultState == NodeResultState.Success)
                {
                    CurrentNode = null;
                    return NodeResult.Processing;
                }
            }

            return result;
        }
    }
}