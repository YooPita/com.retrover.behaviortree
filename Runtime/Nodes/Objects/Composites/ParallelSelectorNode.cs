namespace BananaParty.BehaviorTree
{
    /// <summary>
    /// If Failure or Running then Tick Next else Return "same as child".
    /// </summary>
    public class ParallelSelectorNode : ChainHandlerNode
    {
        protected override string Name => "Parallel Selector Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.ParallelSelector;

        public ParallelSelectorNode(IBehaviorNode[] childNodes) : base(childNodes) { }

        protected override BehaviorNodeStatus OnExecute()
        {
            BehaviorNodeStatus result = BehaviorNodeStatus.Failure;
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                var resultStatus = ChildNodes[i].Execute();

                if (resultStatus == BehaviorNodeStatus.Running)
                    result = BehaviorNodeStatus.Running;

                if (resultStatus == BehaviorNodeStatus.Success)
                    RestartNodesFromIndex(i + 1);
                else continue;

                return resultStatus;
            }
            return result;
        }
    }
}
