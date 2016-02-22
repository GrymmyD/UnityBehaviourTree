using UnityEngine;
using System.Collections;
using System;

public class AmIHurt : Leaf
{
    public int healthThreshold = 100;

    public AmIHurt(int threshold)
    {
        healthThreshold = threshold;
    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if (context.me.livingInfo.GetHealth() < healthThreshold)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
