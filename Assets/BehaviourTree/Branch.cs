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
        public BranchType BranchType;

        public Branch(T _context, BranchType branchType) : base(_context)
        {
            BranchType = branchType;
            PopulateChildren();
        }

        protected NodeResult StartCurrentChild()
        {
            return CurrentNode.Initialise();
        }
        
        protected NodeResult RunCurrentChild()
        {
            return CurrentNode.Process();
        }
    }
    
    [Flags]
    public enum BranchType
    {
        Async,//Runs each node in LIFO, breakOnFailure will bubble up
        Await,//Continuously runs entire sequence in LIFO order, bubbling up on failure
    }
}