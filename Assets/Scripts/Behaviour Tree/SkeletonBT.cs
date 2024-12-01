using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

namespace EnemyNode {

    public class SkeletonBT : BTree
    {
        public Transform patrolPointsParent;
        public static float attackRange = 0.2f;

        protected override Node SetupTree()
        {
            BTree.current = this; // used to easily reference this tree in all nodes
                                // during the whole initialisation phase

            transform.Find("FOVDebug").localScale = Vector3.one * 2f * EnemyUtils.FOV;

            Transform[] patrolPoints = new Transform[patrolPointsParent.childCount];
            for (int i = 0; i < patrolPointsParent.childCount; i++)
                patrolPoints[i] = patrolPointsParent.GetChild(i);

            Node root = new Selector();
            root.SetChildren(new List<Node>()
            {
                new Sequence(new List<Node>()
                {
                    new CheckHasTarget(),
                    new TaskChase(),
                    // new CheckAttackRange(),
                    new TaskAttack(),
                }),
                new TaskPatrol(patrolPoints),
            });

            BTree.current = null;

            return root;
        }
    }

}
