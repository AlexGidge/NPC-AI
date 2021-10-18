using BehaviourTree;
using UnityEngine;

public class FindTarget<T> : Leaf<T> where T : NPCContext
{
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

    public override NodeResult Process()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in possibleTargets)
        {
            context.CharacterMovement.SetLookTarget(go);
            Debug.Log("Target found");
            return NodeResult.Success;
        }
        
        Debug.Log("Failed to find target");
        return NodeResult.Failure;
    }
}

