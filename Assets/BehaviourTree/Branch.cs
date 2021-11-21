using System;
using System.Collections.Generic;

namespace BehaviourTree
{
    /// <summary>
    /// Composite parent node, contains branches & leaves. e.g. Sequence Branch, Random Branch
    /// </summary>
    public abstract class Branch<T> : Node<T> where T : TreeContext
    {
        public Node<T> CurrentNode { get; set; }
        public Stack<Node<T>> Children { get; set; }
        public abstract void PopulateChildren();

        public Branch(T _context) : base(_context)
        {
            PopulateChildren();
        }
        
        protected NodeResult ProcessChild(Node<T> child) 
        {
            if (child != null)
            {
                NodeResult childResult;
                switch (child.CurrentState.ResultState)
                {
                    case NodeResultState.New:
                        childResult = child.Start();
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
    }
}