using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class TargetClosestEnemy : Leaf
{
    float distanceThreshold;
    public TargetClosestEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        
        var holder = new HashSet<MonoBehaviour>();
        foreach (var item in context.me.seenTargets)
        {
            holder.Add(item.GetComponent<RangedEnemyAi>());
        }
        var sortedTargets = holder.ToArray();
        Array.Sort(sortedTargets, delegate (MonoBehaviour item1, MonoBehaviour item2) {
            return Vector3.Distance(context.me.transform.position, item1.transform.position).CompareTo(Vector3.Distance(context.me.transform.position, item2.transform.position));
        });

        foreach(var target in sortedTargets)
        {
            if(Vector3.Distance(context.me.transform.position, target.transform.position) > distanceThreshold) 
            {
                context.enemy = null;
                return NodeStatus.FAILURE;
            } 

            context.enemy = target.gameObject;
            return NodeStatus.SUCCESS;
        }
        context.enemy = null;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
