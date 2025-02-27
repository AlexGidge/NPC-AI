﻿using BehaviourTree;

public class NPCBrain : Tree<NPCContext>
{
    public override Node<NPCContext> GetRootNode()
    {
        return new NPCRootBehaviour<NPCContext>(context);
    }
    
    public NPCBrain(NPCContext context) : base(context)
    {
    }

    public void VisionAlerted()
    {
        base.RestartTree();
    }
}
