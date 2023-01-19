using System.Collections.Generic;

namespace Retrover.BehaviorTree
{
    public class RollbackSelectorNode : RollbackChainHandlerNode
    {
        protected override string Name => "Rollback Selector Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.Selector;

        private readonly List<IBehaviorNode> _failureNodes = new();

        public RollbackSelectorNode(IRollbackNode[] childNodes, bool isContinuous = true) : base(childNodes, isContinuous)
        {
        }

        private RollbackSelectorNode(IRollbackNode[] childNodes, BehaviorNodeStatus status,
            bool isContinuous = true) : base(childNodes, isContinuous)
        {
            _state = status;
        }

        public override IRollbackNode Clone()
        {
            IRollbackNode[] clonedNodes = new IRollbackNode[ChildNodes.Length];
            for (int i = 0; i < clonedNodes.Length; i++)
                clonedNodes[i] = ChildNodes[i].Clone();

            return new RollbackSelectorNode(clonedNodes, _state, IsContinuous);
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            _failureNodes.Clear();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                BehaviorNodeStatus resultStatus = ChildNodes[i].Execute();
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
