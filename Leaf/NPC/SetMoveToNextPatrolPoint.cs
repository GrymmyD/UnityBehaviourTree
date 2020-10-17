using SSG.BehaviourTrees.Primitives;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetMoveToNextPatrolPoint<T> : Leaf<T> where T: NpcContext, IMoveContext, IPatrolContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.MoveTarget = state.PatrolPoints.ElementAt(state.PatrolIndex);
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}

public class SetNextPatrolPoint<T> : Leaf<T> where T: NpcContext, IMoveContext, IPatrolContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.PatrolIndex += 1;
        if (state.PatrolIndex >= state.PatrolPoints.Count)
            state.PatrolIndex = 0;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
