using SSG.BehaviourTrees.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasResourceTarget<T> : Leaf<T> where T : NpcContext, IHasResourceTarget
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.ResourceNode != null) return NodeStatus.SUCCESS;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}

public class HasTarget<T> : Leaf<T> where T : NpcContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (((IHasTarget)state.Me).currentTarget != null)
            return NodeStatus.SUCCESS;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
