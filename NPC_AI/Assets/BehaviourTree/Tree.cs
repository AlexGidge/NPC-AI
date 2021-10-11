namespace BehaviourTree
{
    public abstract class Tree<T> : Node<T> where T : TreeContext
    {
        
        public Node<T> CurrentNode { get; set; }
        
        public Tree(T context) : base(context)
        {
        }
        
        public abstract Node<T> GetRootNode();
        
        public override NodeResult Initialise()
        {
            CurrentNode = GetRootNode();
            return NodeResult.Processing;
        }

        public override NodeResult Process()
        {           
            if (CurrentNode == null)
            {
                CurrentNode = GetRootNode();
            }
            
            return CurrentNode.Process();
        }

    }
}