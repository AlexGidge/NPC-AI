using BehaviourTree;

public class LookAtTarget<T> : Leaf<T> where T : NPCContext
    {
        public LookAtTarget(T _context) : base(_context)
        {
        }
        
        public override NodeResult Process()
        {
            CurrentState = NodeResult.Processing;
            
            if (context.NpcMovement.HasActiveTarget())
            {
                context.NpcMovement.SetRotationForTarget();
                context.NpcMovement.Rotate();//TODO: Success if tollerable distance? Simultaniously run with move node/leaf?
                //TODO: Lose sight distance
            }
            else
            {
                CurrentState = NodeResult.Failure;
            }

            return CurrentState;
        }
    }