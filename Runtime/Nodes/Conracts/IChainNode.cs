namespace BananaParty.BehaviorTree
{
    public interface IChainNode : IBehaviorNode
    {
        void AddNextChainLink(IChainNode nextNode);
    }
}
