using BehaviourTree;
using UnityEngine;

public class MoveToTarget<T> : Leaf<T> where T : NPCContext
    {
        public MoveToTarget(T _context) : base(_context)
        {
        }

        public override NodeResult Initialise()
        {
            //TODO: Start processing? here or in generic?
            if (context.NpcMovement.HasActiveTarget())
            {
                CurrentState =  NodeResult.Processing;
            }
            else
            {
                CurrentState =  NodeResult.Failure;
            }

            return CurrentState;
        }
        
        public override NodeResult Process()
        {
            if (context.NpcMovement.HasActiveTarget())
            {
                context.NpcMovement.MoveInDirectionOfTarget();//TODO: Success if tollerable distance? Simultaniously run with move node/leaf?
                CurrentState =  NodeResult.Processing;
            }
            else
            {
                CurrentState =  NodeResult.Failure;
            }

            return CurrentState;//TODO: Refactor so I don't need to keep doing this return.
        }
    }