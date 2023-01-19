using System.Diagnostics;

namespace Retrover.BehaviorTree
{
    public class TraceWarningNode : BehaviorNode
    {
        protected override string Name => "Trace Warning Node";

        private readonly string _message;

        public TraceWarningNode(string message)
        {
            _message = message;
        }

        protected override BehaviorNodeStatus OnExecute()
        {
            Trace.TraceWarning(_message);
            return BehaviorNodeStatus.Success;
        }
    }
}
