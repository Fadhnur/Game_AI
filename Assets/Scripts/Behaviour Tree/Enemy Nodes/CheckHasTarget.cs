using BehaviorTree;

namespace EnemyNode {

    public class CheckHasTarget : Node
    {
        public override NodeState Evaluate()
        {
            _state = _tree.blackboard.ContainsKey("target")
                ? NodeState.SUCCESS : NodeState.FAILURE;

            _tree.LogNodeState(this.GetType().Name, _state);
            return _state;
        }
    }

}
