using UnityEngine;
using System.Collections;
using System;

public class StopMoving : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        context.me.agent.isStopped = true;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
