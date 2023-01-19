namespace BananaParty.BehaviorTree
{
    public interface INodeDataTextVisualizer
    {
        string Display(BehaviorNodeVisualizationData node);
        string DisplayRoot();
    }
}
