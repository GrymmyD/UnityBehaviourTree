using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class TooCloseToEnemy <T>: Leaf<T> where T : NpcContext, IHasEnemyContext
{
    public float distanceThreshold;

    public TooCloseToEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }
    public override NodeStatus OnBehave(T state)
    {
        if(state.Enemy != null && Vector3.Distance(state.Me.transform.position, state.Enemy.transform.position) < distanceThreshold)
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
