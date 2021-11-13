using BehaviourTree;
using UnityEngine;

public class MoveToTarget<T> : Leaf<T> where T : NPCContext
    {
        public MoveToTarget(T _context) : base(_context)
        {
        }
        
        public override NodeResult Process()
        {
            CurrentState = NodeResult.Processing;
            
            if (context.NpcMovement.HasActiveTarget())
            {
                if (Vector2.Distance(context.NpcMovement.transform.position,
                    context.NpcMovement.TargetObject.transform.position) < context.NpcMovement.VisionLostDistance)
                {
                    context.NpcMovement.MoveInDirectionOfTarget(); 
                    
                    //TODO: Success if tolerable distance? Simultaneously run with move node/leaf?
                }
                else
                {
                    context.NpcMovement.ClearTarget();
                    CurrentState =  NodeResult.Failure;
                }
            }
            else
            {
                CurrentState =  NodeResult.Failure;
            }

            return CurrentState;//TODO: Refactor so I don't need to keep doing this return.
        }
    }