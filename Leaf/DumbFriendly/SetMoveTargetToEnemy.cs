using UnityEngine;
using System.Collections;
using System;

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
