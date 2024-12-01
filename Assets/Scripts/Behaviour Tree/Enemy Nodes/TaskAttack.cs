using UnityEngine;
using UnityEngine.AI;

using BehaviorTree;

namespace EnemyNode {
    public class TaskAttack : Node 
    {
        // private const float _ATTACK_COOLDOWN = 0.5f;
        private NavMeshAgent _agent;
        private Animator _animator;
        // private float _lastAttackTime;
        private float _attackTime = 0.1f;
        private float _attackCounter = 0f;

        private PlayerManager _playerManager;

        public TaskAttack()
        {
            _agent = _tree.GetComponent<NavMeshAgent>();
            _animator = _tree.GetComponent<Animator>();
            // _lastAttackTime = 0f;
        }

        // public override NodeState Evaluate()
        // {
        //     Transform target = (Transform)_tree.blackboard["target"];

        //     // Ensure agent is stopped during attack
        //     _agent.isStopped = true;

        //     // Check attack cooldown
        //     if (Time.time >= _lastAttackTime + _ATTACK_COOLDOWN) {
        //         // Trigger attack animation
        //         _animator.SetTrigger("Attack");
        //         _lastAttackTime = Time.time;
                
        //         // Optional: You might want to apply damage here
        //         // target.GetComponent<PlayerHealth>().TakeDamage(damage);

        //         _state = NodeState.SUCCESS;
        //     } else {
        //         _state = NodeState.RUNNING;
        //     }

        //     return _state;
        // }

        public override NodeState Evaluate()
        {
            // First, check if target is still in attack range
            //(Transform)_tree.blackboard["target"];
            // Transform player;
            Transform player = (Transform)_tree.blackboard["target"];
            // if(EnemyUtils.CheckForPlayerInAttackRange(_tree, _agent, out player)){
            if(Vector3.Distance(_agent.transform.position, player.transform.position) <= SkeletonBT.attackRange){
                // _tree.blackboard["target"] = player;

                if(player != null)
                {
                    _playerManager = player.GetComponentInChildren<PlayerManager>();
                }
                else{
                    Debug.LogWarning("Player Manager tidak ditemukan");
                }

                // Ensure agent is stopped during attack
                _agent.isStopped = true;

                _attackCounter += Time.deltaTime;
                // Debug.Log("count attack: " + _attackCounter);

                // Check attack cooldown
                // if (Time.time >= _lastAttackTime + _ATTACK_COOLDOWN) {
                if (_attackCounter >= _attackTime){
                    // Trigger attack animation
                    _animator.SetTrigger("Attack");
                    // _lastAttackTime = Time.time;
                    
                    // Optional: Apply damage
                    if(_playerManager != null){
                        bool enemyIsDead = _playerManager.TakeHit();
                        Debug.Log("Take Hit: " + enemyIsDead);

                        if(enemyIsDead){
                            _tree.blackboard.Remove("target");
                            _animator.SetBool("Running", true);
                            _agent.isStopped = false;
                        }
                        else{
                            _attackCounter = 0f;
                        }

                        _state = NodeState.SUCCESS;
                    }
                    else{
                        Debug.LogWarning("Player manager null");
                    }

                    
                } 
                else {
                    _state = NodeState.RUNNING;
                }

                // _attackCounter += Time.deltaTime;
                // if (_attackCounter >= _attackTime)
                // {
                //     bool enemyIsDead = _playerManager.TakeHit();
                //     if (enemyIsDead)
                //     {
                //         _tree.blackboard.Remove("target");
                //         // _animator.SetTrigger("Attack");
                //         _animator.SetBool("Running", true);
                //     }
                //     else
                //     {
                //         _attackCounter = 0f;
                //     }
                // }
                // _state = NodeState.RUNNING;
            }
            else{
                _agent.isStopped = false;
                _state = NodeState.FAILURE;
            }

            // Debug.Log("InAttackRange: " + inAttackRange);

            // if (!inAttackRange) {
            //     Debug.Log("OutAttackRange: " + inAttackRange);
            //     _state = NodeState.FAILURE;
            //     return _state;
            // }

            // // Ensure agent is stopped during attack
            // _agent.isStopped = true;

            // // Check attack cooldown
            // if (Time.time >= _lastAttackTime + _ATTACK_COOLDOWN) {
            //     // Trigger attack animation
            //     _animator.SetTrigger("Attack");
            //     _lastAttackTime = Time.time;
                
            //     // Optional: Apply damage
            //     // target.GetComponent<PlayerHealth>().TakeDamage(damage);

            //     _state = NodeState.SUCCESS;
            // } else {
            //     _state = NodeState.RUNNING;
            // }

            _tree.LogNodeState(this.GetType().Name, _state);

            return _state;
        }
    }
}