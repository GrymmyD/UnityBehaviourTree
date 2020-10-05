using SSG.BehaviourTrees.Primitives;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakResource<T> :Leaf<T> where T: NpcContext, IHasResourceTarget
{
    public float BreakRadius { get; set; }
    private bool canFire;
    private float cooldown;
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
            ((IHasTarget)state.Me).currentTarget = null;
            return NodeStatus.SUCCESS;
        }

        if (Vector3.Distance(state.Me.transform.position, state.ResourceNode.transform.position) > BreakRadius)
            return NodeStatus.FAILURE;

        IHasTarget meAsHasTarget = (IHasTarget)state.Me;

        meAsHasTarget.currentTarget= state.ResourceNode.gameObject;
        if (!meAsHasTarget.shouldFire)
        {
            meAsHasTarget.shouldFire = true;
        }
        return NodeStatus.RUNNING;
    }

    public override void OnReset()
    {
        //hasFired = false;
    }
}
