using UnityEngine;
using System.Collections;
using System;

public class CanAttackEnemy<T> : Leaf<T> where T: NpcContext, IHasEnemyContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.Enemy == null)
            return NodeStatus.FAILURE;

        if (!state.Me.CanSee(state.Enemy.gameObject))
            return NodeStatus.FAILURE;

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
