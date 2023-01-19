# Retrover.BehaviorTree
  
Unity package. Fully cross-platform Behavior Tree featuring support for deterministic simulation and prediction-rollback netcode.  
Does not reference Unity Engine, so it could be used in a regular C# project.  

## How to use

### Custom node

```c#
public class MoveCharacterToRandomDirection : BehaviorNode
{
    protected override string Name => "Move To Random Direction";

    IMovableCharacter _character;

    public MoveCharacterToRandomDirection(IMovableCharacter character)
    {
        _character = character;
    }

    protected override BehaviorNodeStatus OnExecute()
    {
        _character.Move(GetRandomAngle());
        return BehaviorNodeStatus.Success;
    }

    private Vector2 GetRandomAngle()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}
```

### Execute tree

```c#
var parallelNode = new SelectorNode(new IBehaviorNode[]
{
    new ConstantNode(BehaviorNodeStatus.Failure),
    new ParallelFirstCompleteNode(new IBehaviorNode[]
    {
        new ConstantNode(BehaviorNodeStatus.Success),
        new ConstantNode(BehaviorNodeStatus.Running),
        new ConstantNode(BehaviorNodeStatus.Success),
    }),
    new ConstantNode(BehaviorNodeStatus.Failure)
});
IBehaviorTreeVisualizer nodeVisualizer = new TextBehaviorTreeVisualizer(this, new TextColoredEmojiNodeDataVisualizer());
IBehaviorTree tree = new UntilSuccessTree(parallelNode, _nodeVisualizer);
tree.Execute();
tree.Visualize();
```

## Installation

Make sure you have standalone [Git](https://git-scm.com/downloads) installed first.

![alt text](https://github.com/YooPita/com.yoopita.retrotvfx/blob/main/DemoImages/installation.png)

And paste this: `https://github.com/YooPita/com.retrover.behaviortree.git`

Or just copy the repository to your project files.