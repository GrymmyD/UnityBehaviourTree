using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class SetMoveTargetToEnemy <T>: Leaf<T> where T : BehaviourState, IHasEnemyContext, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.Enemy == null)
            return NodeStatus.FAILURE;

        state.MoveTarget = state.Enemy.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}

public class SetMoveTargetToTarget <T>: Leaf<T> where T : NpcContext, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (((IHasTarget)state.Me).currentTarget== null)
            return NodeStatus.FAILURE;

        state.MoveTarget = ((IHasTarget)state.Me).currentTarget.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
