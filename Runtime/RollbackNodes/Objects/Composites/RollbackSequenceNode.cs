using System.Collections.Generic;

namespace BananaParty.BehaviorTree
{
    public class RollbackSequenceNode : RollbackChainHandlerNode
    {
        protected override string Name => "Rollback Sequence Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.Sequence;

        private List<IBehaviorNode> _successNodes = new();

        public RollbackSequenceNode(IRollbackNode[] childNodes, bool isContinuous = true) : base(childNodes, isContinuous)
        {
        }

        private RollbackSequenceNode(IRollbackNode[] childNodes, BehaviorNodeStatus status,
            bool isContinuous = true) : base(childNodes, isContinuous)
        {
            _state = status;
        }

        public override IRollbackNode Clone()
        {
            IRollbackNode[] clonedNodes = new IRollbackNode[ChildNodes.Length];
            for (int i = 0; i < clonedNodes.Length; i++)
                clonedNodes[i] = ChildNodes[i].Clone();

            return new RollbackSequenceNode(clonedNodes, _state, IsContinuous);
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            _successNodes.Clear();
            for (int i = 0; i < ChildNodes.Length; i++)
            {
                var resultStatus = ChildNodes[i].Execute();
                if (resultStatus == BehaviorNodeStatus.Success)
                {
                    _successNodes.Add(ChildNodes[i]);
                    continue;
                }

                if (resultStatus == BehaviorNodeStatus.Running && !IsContinuous)
                    RestartSuccessefulNodes();
                else
                    RestartNodesFromIndex(i + 1);
                return resultStatus;
            }
            return BehaviorNodeStatus.Success;
        }

        private void RestartSuccessefulNodes()
        {
            for (int i = 0; i < _successNodes.Count; i++)
            {
                _successNodes[i].Restart();
            }
        }
    }
}
