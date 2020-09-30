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
        FriendlyNpcContext context = (FriendlyNpcContext)state;
        var me = context.me;

        if (me.HP/me.MaxHP < healthThreshold)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
