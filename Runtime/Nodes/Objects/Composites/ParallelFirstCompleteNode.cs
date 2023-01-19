namespace BananaParty.BehaviorTree
{
    /// <summary>
    /// Sequentially runs multiple nodes in parallel and returns as soon as first child completes.
    /// </summary>
    public class ParallelFirstCompleteNode : ChainHandlerNode
    {
        protected override string Name => "Parallel First Complete Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.Decorator;

        public ParallelFirstCompleteNode(IBehaviorNode[] childNodes) : base(childNodes) { }

        protected override BehaviorNodeStatus OnExecute()
        {
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                var resultStatus = ChildNodes[i].Execute();
                if (resultStatus != BehaviorNodeStatus.Running)
                    return resultStatus;
            }
            return BehaviorNodeStatus.Running;
        }
    }
}
