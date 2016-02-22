using UnityEngine;
using System.Collections;
using System;

public class TooCloseToEnemy : Leaf
{
    public float distanceThreshold;

    public TooCloseToEnemy(float threshold)
    {
        distanceThreshold = threshold;
    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if(context.enemy != null && context.me.DistanceTo(context.enemy.transform.position) < distanceThreshold)
        {
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
