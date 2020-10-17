using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class HasEnemy <T>: Leaf<T> where T: BehaviourState,IHasEnemyContext
{
    public override NodeStatus OnBehave(T state)
    {
        if(state.Enemy == null)
        {
            return NodeStatus.FAILURE;
        }

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
