using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class AmIHurt : Leaf<NpcContext>
{
    public float healthThreshold = 100;

    public AmIHurt(float threshold)
    {
        healthThreshold = threshold;
    }
    public override NodeStatus OnBehave(NpcContext state)
    {
        var me = state.Me;

        if (me.HP/me.MaxHP < healthThreshold)
            return NodeStatus.SUCCESS;
        else
            return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
