using UnityEngine;
using System.Collections;
using System;

public class CanAttackEnemy : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if (context.enemy == null)
            return NodeStatus.FAILURE;

        if (!context.me.CanSee(context.enemy.gameObject))
            return NodeStatus.FAILURE;

        if (context.me.GetComponent<Player_Attack>().CanAttack())
            return NodeStatus.SUCCESS;

        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
