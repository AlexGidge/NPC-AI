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
        
        public override NodeResult Initialise()
        {
            CurrentNode = GetRootNode();
            return NodeResult.Processing;
        }

        public override NodeResult Process()
        {
            if (CurrentNode == null)
            {
                CurrentNode = GetRootNode();
            }

            switch (CurrentNode.CurrentState.ResultState)
            {
                case NodeResultState.New:
                    return CurrentNode.Initialise();
                case NodeResultState.Success:
                    Debug.Log("NPC tree completed. Restarting sequence."); //TODO: Tree success
                    CurrentNode = null;
                    return NodeResult.Success;
                case NodeResultState.Failure:
                    Debug.Log("Failure at NPC tree. Restarting sequence."); //TODO: Tree failure
                    CurrentNode = null;
                    return NodeResult.Failure;
                case NodeResultState.Processing:
                    return CurrentNode.Process(); //TODO: Async
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}