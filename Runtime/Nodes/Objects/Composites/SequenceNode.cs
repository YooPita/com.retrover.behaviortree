using System.Collections.Generic;

namespace BananaParty.BehaviorTree
{
    /// <summary>
    /// If Success then Tick Next else Return "same as child".
    /// </summary>
    public class SequenceNode : ChainHandlerNode
    {
        protected override string Name => "Sequence Node";

        protected override BehaviorNodeType Type => BehaviorNodeType.Sequence;

        private List<IBehaviorNode> _successNodes = new();

        public SequenceNode(IBehaviorNode[] childNodes, bool isContinuous = true) : base(childNodes, isContinuous)
        {
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
