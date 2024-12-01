using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace EnemyNode {

    public class TaskChase : Node
    {
        private const float _REACH_THRESHOLD = 1f;

        private NavMeshAgent _agent;
        private Animator _animator;
        private bool _running = false;

        public TaskChase()
        {
            _agent = _tree.GetComponent<NavMeshAgent>();
            _animator = _tree.GetComponent<Animator>();
        }

        // public override NodeState Evaluate()
        // {
        //     Transform player;
        //     if (EnemyUtils.CheckForPlayerInFOV(_tree, _agent, out player)) {
        //         _tree.blackboard["target"] = player;
        //         _agent.SetDestination(player.position);
        //     }

        //     //&& _agent.transform.position != player.position
        //     if (!_running) {
        //         Transform target = (Transform)_tree.blackboard["target"];
        //         _agent.SetDestination(target.position);
        //         _animator.SetBool("Running", true);
        //         _agent.isStopped = false;
        //         _running = true;
        //         _state = NodeState.RUNNING;
        //     }

        //     else if (_agent.remainingDistance < _REACH_THRESHOLD) {
        //         _tree.blackboard.Remove("target");
        //         _tree.blackboard["reached_target"] = true;
        //         _animator.SetBool("Running", false);
        //         if(_agent.stoppingDistance < _REACH_THRESHOLD)
        //         {
        //             _agent.isStopped = true;
        //         }
        //         _running = false;
        //         _state = NodeState.SUCCESS;
        //     }

        //     _tree.LogNodeState(this.GetType().Name, _state);

        //     return _state;
        // }

        public override NodeState Evaluate()
        {
            Transform player;
            if (EnemyUtils.CheckForPlayerInFOV(_tree, _agent, out player)) {
                _tree.blackboard["target"] = player;
                
                // Hitung jarak antara agen dan player
                float distanceToPlayer = Vector3.Distance(_agent.transform.position, player.position);
                
                // Jika jarak kurang dari atau sama dengan stopping distance
                if (distanceToPlayer <= _agent.stoppingDistance) {
                    _agent.isStopped = true;
                    _animator.SetBool("Running", false);
                    _state = NodeState.SUCCESS;
                    return _state;
                }
                
                // Lanjutkan pergerakan menuju player
                _agent.SetDestination(player.position);
            }

            if (!_running) {
                Transform target = (Transform)_tree.blackboard["target"];
                _agent.SetDestination(target.position);
                _animator.SetBool("Running", true);
                _agent.isStopped = false;
                _running = true;
                _state = NodeState.RUNNING;
            }

            else if (_agent.remainingDistance < _REACH_THRESHOLD) {
                _tree.blackboard.Remove("target");
                _tree.blackboard["reached_target"] = true;
                _animator.SetBool("Running", false);
                _agent.isStopped = true;
                _running = false;
                _state = NodeState.SUCCESS;
            }

            _tree.LogNodeState(this.GetType().Name, _state);

            return _state;
        }

    }

}
