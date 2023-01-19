using System.Collections.Generic;

namespace BananaParty.BehaviorTree
{
    public abstract class RollbackChainHandlerNode : BehaviorNode, IRollbackNode
    {
        protected bool IsContinuous { private set; get; }

        protected IRollbackNode[] ChildNodes { private set; get; }

        public RollbackChainHandlerNode(IRollbackNode[] childNodes, bool isContinuous = true)
        {
            IsContinuous = isContinuous;
            ChildNodes = childNodes;
        }

        protected RollbackChainHandlerNode() { }

        public override BehaviorNodeVisualizationData GetVisualizationData()
        {
            List<BehaviorNodeVisualizationData> chain = new List<BehaviorNodeVisualizationData>();

            for (int i = 0; i < ChildNodes.Length; i++)
                chain.Add(ChildNodes[i].GetVisualizationData());

            for (int i = 0; i < chain.Count - 1; i++)
                chain[i].NextNode = chain[i + 1];

            return new BehaviorNodeVisualizationData()
            {
                Name = Name,
                State = _state,
                Type = Type,
                ChildNode = chain[0],
            };
        }

        public abstract IRollbackNode Clone();

        protected override void OnRestart()
        {
            for (int i = 0; i < ChildNodes.Length; i++)
                ChildNodes[i].Restart();
        }

        protected void RestartNodesFromIndex(int restartIndex)
        {
            for (int i = restartIndex; i < ChildNodes.Length; i++)
                ChildNodes[i].Restart();
        }
    }
}
