using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMoveTargetToResource <T>: Leaf<T> where T :NpcContext,IHasResourceTarget,IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.ResourceNode == null)
            return NodeStatus.FAILURE;

        state.MoveTarget = state.ResourceNode.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
