using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosestResource<T> : Leaf<T> where T: NpcContext, IHasResourceTarget
{
    public float Threshhold { get; set; }

    public TargetClosestResource(float threshhold)
    {
        Threshhold = threshhold;
    }

    public override NodeStatus OnBehave(T state)
    {
        var meAsHasTarget = (IHasTarget)state.Me;
        var holder = new HashSet<ResourceNode>();
        foreach (var item in ((IHasTargetList) state.Me).seenTargets)
        {
            if (item == null) continue;
            holder.Add(item.GetComponent<ResourceNode>());
        }
        holder.RemoveWhere(x => x == null);
        var sortedTargets = holder.ToArray();
        Array.Sort(sortedTargets, delegate (MonoBehaviour item1, MonoBehaviour item2) {
            return Vector3.Distance(state.Me.transform.position, item1.transform.position).CompareTo(Vector3.Distance(state.Me.transform.position, item2.transform.position));
        });

        foreach (var target in sortedTargets)
        {
            if (Vector3.Distance(state.Me.transform.position, target.transform.position) > Threshhold)
            {
                return NodeStatus.FAILURE;
            }

            meAsHasTarget.currentTarget = target.gameObject;
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
