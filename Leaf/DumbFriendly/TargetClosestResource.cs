using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetClosestResource : Leaf
{
    public float Threshhold { get; set; }

    public TargetClosestResource(float threshhold)
    {
        Threshhold = threshhold;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        var holder = new HashSet<ResourceNode>();
        foreach (var item in context.me.seenTargets)
        {
            if (item == null) continue;
            holder.Add(item.GetComponent<ResourceNode>());
        }
        holder.RemoveWhere(x => x == null);
        var sortedTargets = holder.ToArray();
        Array.Sort(sortedTargets, delegate (MonoBehaviour item1, MonoBehaviour item2) {
            return Vector3.Distance(context.me.transform.position, item1.transform.position).CompareTo(Vector3.Distance(context.me.transform.position, item2.transform.position));
        });

        foreach (var target in sortedTargets)
        {
            if (Vector3.Distance(context.me.transform.position, target.transform.position) > Threshhold)
            {
                context.ResourceNode = null;
                return NodeStatus.FAILURE;
            }

            context.ResourceNode = target.GetComponent<ResourceNode>();
            return NodeStatus.SUCCESS;
        }
        context.ResourceNode = null;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
