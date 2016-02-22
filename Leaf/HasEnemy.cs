using UnityEngine;
using System.Collections;
using System;

public class HasEnemy : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if(context.enemy == null)
        {
            return NodeStatus.FAILURE;
        }

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
