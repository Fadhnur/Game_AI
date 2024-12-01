using UnityEngine;
using BehaviorTree;

namespace EnemyNode {
    public class CheckAttackRange : Node 
    {
        // private const float _ATTACK_RANGE = 0.5f;

        private Animator _animator;

        public CheckAttackRange()
        {
            _animator = _tree.GetComponent<Animator>();
        }

        // public override NodeState Evaluate()
        // {
        //     // Check if target exists in blackboard
        //     if (!_tree.blackboard.ContainsKey("target")) {
        //         _state = NodeState.FAILURE;
        //         return _state;
        //     }

        //     Transform target = (Transform)_tree.blackboard["target"];
        //     float distanceToTarget = Vector3.Distance(_tree.transform.position, target.position);

        //     // Check if target is within attack range
        //     if (distanceToTarget <= _ATTACK_RANGE) {
        //         _animator.SetBool("Running", false);

        //         _state = NodeState.SUCCESS;
        //     } else {
        //         _state = NodeState.FAILURE;
        //     }

        //     return _state;
        // }

        public override NodeState Evaluate()
        {
            Transform target;
            if (EnemyUtils.CheckForPlayerInAttackRange(_tree, null, out target)) {
                _tree.blackboard["target"] = target;
                _animator.SetBool("Running", false);
                // _animator.SetTrigger("Attack");
                _state = NodeState.SUCCESS;
            } 
            // else {
            //     _state = NodeState.FAILURE;
            // }

            // return _state;

            // Existing logic
            _tree.LogNodeState(this.GetType().Name, _state);
            _state = NodeState.FAILURE;
            return _state;
        }
    }
}