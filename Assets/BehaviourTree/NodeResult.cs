﻿namespace BehaviourTree
{
    public class NodeResult
    {
        public static NodeResult Processing {get
            {
                return new NodeResult(NodeResultState.Processing);
            }
        }
        
        public static NodeResult Failure {get
            {
                return new NodeResult(NodeResultState.Failure);
            }
        }

        public static NodeResult Success
        {
            get { return new NodeResult(NodeResultState.Success); }
        }
        
        public static NodeResult New
        {
            get { return new NodeResult(NodeResultState.New); }
        }

        public NodeResult(NodeResultState result)
        {
            ResultState = result;
        }

        public NodeResultState ResultState { get; private set; }
    }//TODO: AreEqual / Comparison to just check status?

    public enum NodeResultState
    {
        New = 0,
        Success = 1,
        Failure = 2,
        Processing = 3
    }
}