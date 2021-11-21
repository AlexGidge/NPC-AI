using System.Collections.Generic;
using BehaviourTree;
public class PlayBall<T> : SequenceBranch<T> where T : NPCContext
{
    public PlayBall(T _context) : base(_context)
    {
    }

    public override NodeResult Start()
    {
        return Process();
    }

    public override void PopulateChildren()
    {
        Children = new Stack<Node<T>>();
        //Get Ball

    }
}