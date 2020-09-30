using UnityEngine;
using System.Collections;
using System;

public class AttackEnemy : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        var enemy = context.enemy;
        var targetInfo = enemy.GetComponent<ITargetable>();
        if (enemy == null || targetInfo == null)
            return NodeStatus.FAILURE;

        context.me.LookAt(context.enemy.transform.position + targetInfo.ExternalTargetOffset);
        context.me.Animator.Play("Shooting");

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
