using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class TargetClosestEnemy<T> : Leaf<T> where T: NpcContext, IHasEnemyContext
{
    float distanceThreshold;
    public TargetClosestEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(T state)
    {
        var holder = new HashSet<MonoBehaviour>();
        foreach (var item in ((IHasTargetList)state.Me).seenTargets)
        {
            holder.Add(item.GetComponent<RangedEnemyAi>());
        }
        var sortedTargets = holder.ToArray();
        Array.Sort(sortedTargets, delegate (MonoBehaviour item1, MonoBehaviour item2) {
            return Vector3.Distance(state.Me.transform.position, item1.transform.position).CompareTo(Vector3.Distance(state.Me.transform.position, item2.transform.position));
        });

        foreach(var target in sortedTargets)
        {
            if(Vector3.Distance(state.Me.transform.position, target.transform.position) > distanceThreshold) 
            {
                state.Enemy = null;
                return NodeStatus.FAILURE;
            } 

            state.Enemy = target.gameObject;
            return NodeStatus.SUCCESS;
        }
        state.Enemy = null;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
