namespace BananaParty.BehaviorTree
{
    /// <summary>
    /// If Success or Running then Tick Next else Return "same as child".
    /// </summary>
    public class ParallelSequenceNode : ChainHandlerNode
    {
        protected override string Name => "Parallel Sequence Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.ParallelSequence;

        public ParallelSequenceNode(IBehaviorNode[] childNodes) : base(childNodes) { }

        protected override BehaviorNodeStatus OnExecute()
        {
            BehaviorNodeStatus result = BehaviorNodeStatus.Success;
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                BehaviorNodeStatus resultStatus = ChildNodes[i].Execute();

                if (resultStatus == BehaviorNodeStatus.Running)
                    result = BehaviorNodeStatus.Running;

                if (resultStatus == BehaviorNodeStatus.Failure)
                    RestartNodesFromIndex(i + 1);
                else continue;

                return resultStatus;
            }
            return result;
        }
    }
}
