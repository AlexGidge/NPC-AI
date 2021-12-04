using System;
using BehaviourTree;
using UnityEditor;
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
        if (other.CompareTag("Player") || other.CompareTag("Ball"))//TODO: Improve player detection
        {
            npcBrain.VisionAlerted();
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(NPC))]
public class LevelScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        if (target != null)
        {
            NPC npc = (NPC) target;
            EditorGUILayout.LabelField("Current State", Enum.GetName(typeof(NodeResult), npc.npcBrain.CurrentState));
            EditorGUILayout.LabelField("Current Node", npc.npcBrain.CurrentNode.GetType().ToString());
            EditorGUILayout.LabelField("Context", npc.npcBrain.CurrentNode.context.ToString());
        }
    }
}


#endif
