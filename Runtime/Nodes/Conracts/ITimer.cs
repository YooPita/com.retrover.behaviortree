namespace Retrover.BehaviorTree
{
    public interface ITimer
    {
        void Start();
        void Reset();
        bool IsEnded();
    }
}
