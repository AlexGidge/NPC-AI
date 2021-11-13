using BehaviourTree;

public class NPCBrain : Tree<NPCContext>
{
    public override Node<NPCContext> GetRootNode()
    {
        return new HuntSequence<NPCContext>(context);
    }
    
    public NPCBrain(NPCContext context) : base(context)
    {
    }
}
