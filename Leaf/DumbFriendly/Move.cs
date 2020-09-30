using UnityEngine;
using System.Collections;
using System;

public class Move : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (IMoveContext)state;
        if (!context.MoveTarget.HasValue)
            return NodeStatus.FAILURE;

        if(starting)
        {
            context.me.agent.destination = context.moveTarget.Value;
            if (AtDestination(context))
                return NodeStatus.SUCCESS;
        }

        if(context.me.agent.path != null)
        {
            if (AtDestination(context))
                return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }

    bool AtDestination(FriendlyNpcContext context)
    {
        // If we're already really close, don't bother pathing
        if (context.me.agent.remainingDistance < 2.0f)
        {
            context.me.LookAt(context.moveTarget.Value);
            return true;
        }
        return false;
    }

    public override void OnReset()
    {
    }
}