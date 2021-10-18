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
            if (context.CharacterMovement.HasActiveTarget())
            {
                return NodeResult.Processing;
            }
            else
            {
                return NodeResult.Failure;
            }
        }
        
        public override NodeResult Process()
        {
            if (context.CharacterMovement.HasActiveTarget())
            {
                context.CharacterMovement.SetRotationForTarget();
                context.CharacterMovement.Rotate();//TODO: Success if tollerable distance? Simultaniously run with move node/leaf?
                return NodeResult.Processing;
            }
            else
            {
                return NodeResult.Failure;
            }
        }
    }