using UnityEngine;
using System.Collections;
using System;

public class FindClosestHeal : Leaf
{
    float distanceThreshold;
    public FindClosestHeal(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        Heal[] heals = context.me.dynamicEntities.GetComponentsInChildren<Heal>();

        if(heals.Length == 0)
        {
            context.moveTarget = null;
            return NodeStatus.FAILURE;
        }

        Array.Sort(heals, delegate (Heal h1, Heal h2)
        {
            float d1 = context.me.DistanceTo(h1.transform.position);
            float d2 = context.me.DistanceTo(h2.transform.position);

            return d1.CompareTo(d2);
        });

        if (context.me.DistanceTo(heals[0].transform.position) > distanceThreshold)
            return NodeStatus.FAILURE;

        context.moveTarget = heals[0].transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
