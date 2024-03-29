﻿namespace Retrover.BehaviorTree.Tests
{
    public class InvocationTestNode : BehaviorNode
    {
        public int ExecutionCount { get; private set; } = 0;
        public BehaviorNodeStatus ResultStatus { get; set; } = BehaviorNodeStatus.Idle;

        protected override string Name => "Invocation Test Node";

        public InvocationTestNode(BehaviorNodeStatus statusToReturn)
        {
            ResultStatus = statusToReturn;
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            ExecutionCount += 1;
            return ResultStatus;
        }
    }
}
