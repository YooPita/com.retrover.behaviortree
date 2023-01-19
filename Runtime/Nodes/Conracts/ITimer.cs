namespace BananaParty.BehaviorTree
{
    public interface ITimer
    {
        void Start();
        void Reset();
        bool IsEnded();
    }
}
