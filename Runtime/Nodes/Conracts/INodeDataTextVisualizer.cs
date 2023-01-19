namespace Retrover.BehaviorTree
{
    public interface INodeDataTextVisualizer
    {
        string Display(BehaviorNodeVisualizationData node);
        string DisplayRoot();
    }
}
