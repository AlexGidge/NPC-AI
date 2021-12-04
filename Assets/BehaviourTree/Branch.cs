using System;
using System.Collections.Generic;
using UnityEngine;

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
            if (child == null)
            {
                throw new ArgumentException("ProcessChild: Child was null",nameof(child));
            }

            switch (child.Process().ResultState)
            {
                case NodeResultState.New:
                    return child.Start();
                case NodeResultState.Success:
                    return NodeResult.Success;
                case NodeResultState.Failure:
                    return NodeResult.Failure;
                case NodeResultState.Processing:
                    return child.Process();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}