using Unity;
using BehaviourTree;
using UnityEngine;

public class Idle<T> : Node<T> where T : NPCContext
{
    private float startTime;

    private Vector2 rotationVector;
    
    public Idle(T _context) : base(_context)
    {
    }

    public override NodeResult Start()
    {
        startTime = Time.time;
        rotationVector = new Vector2(Random.Range(-360f, 360f), Random.Range(-360f, 360f));
        return Process();
    }

    public override NodeResult Process()
    {
        //TODO: Idle movement around room for x seconds
        if (Time.time < startTime + context.IdleTime)
        {
            context.NpcMovement.LookInDirection(rotationVector);
            context.NpcMovement.MoveInDirectionOfTarget();
            CurrentState = NodeResult.Processing;
        }
        else
        {
            CurrentState = NodeResult.Success;
        }

        return CurrentState;
    }
}