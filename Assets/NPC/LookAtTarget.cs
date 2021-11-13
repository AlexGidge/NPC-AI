using BehaviourTree;
using UnityEngine;

public class LookAtTarget<T> : Leaf<T> where T : NPCContext
    {
        public LookAtTarget(T _context) : base(_context)
        {
        }

        public override NodeResult Initialise()
        {
            //TODO: Start processing? here or in generic?
            if (context.NpcMovement.HasActiveTarget())
            {
                CurrentState = NodeResult.Processing;
            }
            else
            {
                CurrentState = NodeResult.Failure;
            }

            return CurrentState;
        }
        
        public override NodeResult Process()
        {
            if (context.NpcMovement.HasActiveTarget())
            {
                context.NpcMovement.SetRotationForTarget();
                context.NpcMovement.Rotate();//TODO: Success if tollerable distance? Simultaniously run with move node/leaf?
                //TODO: Lose sight distance
                CurrentState = NodeResult.Processing;
            }
            else
            {
                CurrentState = NodeResult.Failure;
            }

            return CurrentState;
        }
    }