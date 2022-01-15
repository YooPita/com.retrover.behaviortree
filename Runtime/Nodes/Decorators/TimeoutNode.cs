﻿namespace BananaParty.BehaviorTree
{
    public class TimeoutNode : DecoratorNode
    {
        private readonly long _duration;

        private long _startTime = -1;

        public TimeoutNode(IBehaviorNode childNode, long duration) : base(childNode)
        {
            _duration = duration;
        }

        public override string Name => $"{base.Name} {_duration}";

        public override BehaviorNodeStatus OnExecute(long time)
        {
            if (Status != BehaviorNodeStatus.Running)
                _startTime = time;

            if (time < _startTime + _duration)
            {
                return ChildNode.Execute(time);
            }
            else
            {
                ChildNode.Reset();
                return BehaviorNodeStatus.Failure;
            }
        }

        public override void Reset()
        {
            base.Reset();

            _startTime = -1;
        }
    }
}
