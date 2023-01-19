using System.Collections.Generic;

namespace BananaParty.BehaviorTree
{
    /// <summary>
    /// If Failure then Tick Next else Return "same as child".
    /// </summary>
    public class SelectorNode : ChainHandlerNode
    {
        protected override string Name => "Selector Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.Selector;

        private List<IBehaviorNode> _failureNodes = new();

        public SelectorNode(IBehaviorNode[] childNodes, bool isContinuous = true) : base(childNodes, isContinuous)
        {
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            _failureNodes.Clear();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                var resultStatus = ChildNodes[i].Execute();
                if (resultStatus == BehaviorNodeStatus.Failure)
                {
                    _failureNodes.Add(ChildNodes[i]);
                    continue;
                }

                if (resultStatus == BehaviorNodeStatus.Running && !IsContinuous)
                    RestartFailedNodes();
                else
                    RestartNodesFromIndex(i + 1);
                return resultStatus;
            }
            return BehaviorNodeStatus.Failure;
        }

        private void RestartFailedNodes()
        {
            for (int i = 0; i < _failureNodes.Count; i++)
            {
                _failureNodes[i].Restart();
            }
        }
    }
}
