using UnityEngine;
using System.Collections;
using System;

public class AttackEnemy<T> : Leaf<T> where T: NpcContext, IHasEnemyContext
{
    public override NodeStatus OnBehave(T state)
    {
        var enemy = state.Enemy;
        var targetInfo = enemy.GetComponent<ITargetable>();
        if (enemy == null || targetInfo == null)
            return NodeStatus.FAILURE;

        state.Me.LookAt(state.Enemy.transform.position + targetInfo.ExternalTargetOffset);
        state.Me.Animator.Play("Shooting");

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
