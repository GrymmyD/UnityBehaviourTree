using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakResource<T> :Leaf<T> where T: NpcContext, IHasResourceTarget
{
    public float BreakRadius { get; set; }

    public BreakResource(float breakRadius = 10f)
    {
        BreakRadius = breakRadius;
    }

    public override NodeStatus OnBehave(T state)
    {
        if (starting && state.ResourceNode == null)
            return NodeStatus.FAILURE;
        else if (state.ResourceNode == null)
        {
            state.ResourceNode = null;
            return NodeStatus.SUCCESS;
        }

        if (Vector3.Distance(state.Me.transform.position, state.ResourceNode.transform.position) > BreakRadius)
            return NodeStatus.FAILURE;

        ((IHasTarget)state.Me).currentTarget= state.ResourceNode.gameObject;
        state.Me.Animator.SetTrigger("Shoot");
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
