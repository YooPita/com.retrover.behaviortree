namespace Retrover.BehaviorTree
{
    public interface IRollbackNode : IBehaviorNode
    {
        IRollbackNode Clone();
    }
}
