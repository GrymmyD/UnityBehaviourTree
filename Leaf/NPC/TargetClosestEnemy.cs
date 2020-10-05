using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using SSG.BehaviourTrees.Primitives;

public class TargetClosestEnemy<T> : Leaf<T> where T: NpcContext, IHasEnemyContext
{
    float distanceThreshold;
    public TargetClosestEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(T state)
    {
        var holder = new HashSet<GameObject>();
        foreach (var item in ((IHasTargetList)state.Me).seenTargets)
        {
            if (item == null) continue;
            var factionInfo = item.GetComponent<IHasFaction>();
            if (state.Me.IsEnemy(factionInfo))
            {
                holder.Add(item);
            }
        }
        var sortedTargets = holder.ToArray();
        Array.Sort(sortedTargets, delegate (GameObject item1, GameObject item2) {
            return Vector3.Distance(state.Me.transform.position, item1.transform.position).CompareTo(Vector3.Distance(state.Me.transform.position, item2.transform.position));
        });

        if (sortedTargets.Length < 1 ||
            Vector3.Distance(state.Me.transform.position, sortedTargets[0].transform.position) > distanceThreshold)
        {
            return NodeStatus.FAILURE;
        }

        ((IHasTarget) state.Me).currentTarget = sortedTargets[0].gameObject;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
