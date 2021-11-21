using System;
using UnityEngine;

    /// <summary>
    /// NPC Director
    /// </summary>
public class NPC : MonoBehaviour
{
    public Rigidbody2D NPCRigidBody;
    public float NPCMoveSpeed;
    public float NPCRotationSpeed;
    public float NPCIdleTime;
    public NPCMovement NPCMovement;
    public NPCBrain npcBrain;
    public NPCContext npcContext;
    
    private void OnEnable()
    {
        RegisterEvents();
        Initialise();
    }

    private void Initialise()
    {
        NPCMovement.Initialise(NPCRigidBody, NPCMoveSpeed, NPCRotationSpeed);
        
        npcContext = GenerateNPCContext();
        npcBrain = new NPCBrain(npcContext);
        npcBrain.Start();

    }

    private void OnDisable()
    {
        EngineManager.Current.Events.EveryUpdate -= Tick;
    }

    private void RegisterEvents()
    {
        EngineManager.Current.Events.EveryUpdate += Tick;
    }

    private void Tick()
    {
       npcBrain.Process();
    }

    private NPCContext GenerateNPCContext()
    {
        NPCContext context = new NPCContext()
        {
            NpcMovement = NPCMovement,
            IdleTime = NPCIdleTime
        };
        return context;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))//TODO: Improve player detection
        {
            npcBrain.VisionAlerted();
        }
    }
}
