using UnityEngine;

public class NPC : MonoBehaviour
{
    public Rigidbody2D NPCRigidBody;
    public float NPCMoveSpeed;
    public float NPCRotationSpeed;
    public CharacterMovement NPCMovement;
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
        npcBrain.Initialise();

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
            CharacterMovement = NPCMovement
        };
        return context;
    }
}
