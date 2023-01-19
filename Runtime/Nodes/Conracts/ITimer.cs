namespace BananaParty.BehaviorTree
{
    public interface ITimer
    {
        void StartIfNot();
        void Reset();
        bool IsEnded();
    }
}
