using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

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

public class CanAttackTarget<T> : Leaf<T> where T: NpcContext
{
    public override NodeStatus OnBehave(T state)
    {
        var target = ((IHasTarget)state.Me).currentTarget;
        if (target == null)
            return NodeStatus.FAILURE;

        if (!state.Me.CanSee(target))
            return NodeStatus.FAILURE;

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
