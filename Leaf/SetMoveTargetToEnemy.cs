using UnityEngine;
using System.Collections;
using System;

public class SetMoveTargetToEnemy : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if (context.enemy == null)
            return NodeStatus.FAILURE;

        context.moveTarget = context.enemy.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
