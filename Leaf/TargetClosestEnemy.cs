using UnityEngine;
using System.Collections;
using System;

public class TargetClosestEnemy : Leaf
{
    float distanceThreshold;
    public TargetClosestEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        Living[] livingObjects = context.me.players.GetComponentsInChildren<Living>();
        Array.Sort(livingObjects, delegate (Living l1, Living l2)
        {
            float d1 = context.me.DistanceTo(l1.transform.position);
            float d2 = context.me.DistanceTo(l2.transform.position);

            return d1.CompareTo(d2);
        });

        foreach(Living l in livingObjects)
        {
            if(l.gameObject != context.me.gameObject)
            {
                if(context.me.DistanceTo(l.gameObject.transform.position) > distanceThreshold)
                {
                    context.enemy = null;
                    return NodeStatus.FAILURE;
                } 

                context.enemy = l;
                return NodeStatus.SUCCESS;
            }
        }
        context.enemy = null;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
