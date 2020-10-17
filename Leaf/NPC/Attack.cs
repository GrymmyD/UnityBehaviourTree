using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Attack<T> : Leaf<T> where T: NpcContext 
{
    public Attack(float breakRadius)
    {
        BreakRadius = breakRadius;
    }

    public float BreakRadius { get; set; }
    public override NodeStatus OnBehave(T state)
    {
        try
        {
            var meWithTargetInfo = (IHasTarget)state.Me;
            if (starting && meWithTargetInfo.currentTarget == null)
                return NodeStatus.FAILURE;
            else if (meWithTargetInfo.currentTarget == null)
            {
                ((IHasTarget)state.Me).currentTarget = null;
                return NodeStatus.SUCCESS;
            }

            if (Vector3.Distance(state.Me.transform.position, meWithTargetInfo.currentTarget.transform.position) > BreakRadius)
                return NodeStatus.FAILURE;

            IHasTarget meAsHasTarget = (IHasTarget)state.Me;

            meAsHasTarget.currentTarget= meWithTargetInfo.currentTarget;
            if (!meAsHasTarget.shouldFire)
            {
                meAsHasTarget.shouldFire = true;
            }
            return NodeStatus.RUNNING;
        }
        catch (Exception)
        {
            return NodeStatus.FAILURE;
        }
    }

    public override void OnReset()
    {
    }
}
