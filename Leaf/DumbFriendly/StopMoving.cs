using UnityEngine;
using System.Collections;
using System;

public class StopMoving <T>: Leaf<T> where T : NpcContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.Me.NavMeshAgent.isStopped = true;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
