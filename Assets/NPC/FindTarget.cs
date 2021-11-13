using BehaviourTree;
using UnityEngine;

public class FindTarget<T> : Leaf<T> where T : NPCContext
{
    public float VisionRange;

    public FindTarget(T _context) : base(_context)
    {
    }

    public override NodeResult Initialise()
    {
        Debug.Log("Finding target...");
        if (context.CharacterMovement.HasActiveTarget())
        {
            return NodeResult.Success;
        }
        else return Process();
    }

    public override NodeResult Process()//TODO: Process on collision?
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in possibleTargets)
        {
            if (Vector2.Distance(context.CharacterMovement.transform.position, go.transform.position) <
                context.CharacterMovement.VisionRange)
            {
                context.CharacterMovement.SetLookTarget(go);
                Debug.Log("Target found");
                return NodeResult.Success;
            }
        }
        
        Debug.Log("Failed to find target");
        return NodeResult.Failure;
    }
}

