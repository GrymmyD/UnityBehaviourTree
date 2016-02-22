using UnityEngine;
using System.Collections;
using System;

public class FindClosestPowerup : Leaf
{
    float distanceThreshold;
    public FindClosestPowerup(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        Item[] powerups = context.me.dynamicEntities.GetComponentsInChildren<Item>();

        if (powerups.Length == 0)
        {
            context.moveTarget = null;
            return NodeStatus.FAILURE;
        }

        Array.Sort(powerups, delegate (Item h1, Item h2)
        {
            float d1 = context.me.DistanceTo(h1.transform.position);
            float d2 = context.me.DistanceTo(h2.transform.position);

            return d1.CompareTo(d2);
        });

        if (context.me.DistanceTo(powerups[0].transform.position) > distanceThreshold)
            return NodeStatus.FAILURE;

        context.moveTarget = powerups[0].transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
