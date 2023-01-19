namespace BananaParty.BehaviorTree
{
    public interface IRollbackChainNode : IBehaviorNode
    {
        void AddNextChainLink(IRollbackChainNode nextNode);
        IRollbackChainNode Clone();
    }
}
