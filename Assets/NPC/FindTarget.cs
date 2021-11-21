using BehaviourTree;
using UnityEngine;

public class FindTarget<T> : Leaf<T> where T : NPCContext
{
    public FindTarget(T _context) : base(_context)
    {
    }

    public override NodeResult Start()
    {
        Debug.Log("Finding target...");
        if (context.NpcMovement.HasActiveTarget())
        {
            CurrentState = NodeResult.Success;
        }
        else CurrentState = Process();

        return CurrentState;
    }

    public override NodeResult Process()//TODO: Run on collision?
    {
        CurrentState = NodeResult.Processing;
        
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in possibleTargets)
        {
            if (Vector2.Distance(context.NpcMovement.transform.position, go.transform.position) <
                context.NpcMovement.VisionRange)
            {
                context.NpcMovement.SetLookTarget(go);
                Debug.Log("Target found");
                CurrentState = NodeResult.Success;
                //TODO: Handling multiple target options
            }
            else
            {
                context.NpcMovement.ClearTarget();
                CurrentState = NodeResult.Failure;
            }
        }

        return CurrentState;
    }
}

