using System;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree<T> : Node<T> where T : TreeContext
    {
        
        public Node<T> CurrentNode { get; set; }
        
        public Tree(T context) : base(context)
        {
        }
        
        public abstract Node<T> GetRootNode();
        
        public override NodeResult Start()
        {
            CurrentNode = GetRootNode();
            CurrentState = NodeResult.New;
            return CurrentState;
        }

        public override NodeResult Process()
        {
            if (CurrentNode == null)
            {
                Start();
            }

            switch (CurrentNode.CurrentState.ResultState)
            {
                case NodeResultState.New:
                    return CurrentNode.Start();
                case NodeResultState.Success:
                    Debug.Log("NPC tree completed. Restarting sequence."); //TODO: Tree success
                    RestartTree();
                    return NodeResult.Success;
                case NodeResultState.Failure:
                    Debug.Log("Failure at NPC tree. Restarting sequence."); //TODO: Tree failure
                    CurrentNode = null;
                    RestartTree();
                    return NodeResult.Failure;
                case NodeResultState.Processing:
                    return CurrentNode.Process(); //TODO: Async
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void RestartTree()
        {
            CurrentNode = null;
        }
    }
}