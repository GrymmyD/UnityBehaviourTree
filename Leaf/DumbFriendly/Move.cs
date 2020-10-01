using UnityEngine;
using System.Collections;
using System;

public class Move<T> : Leaf<T> where T: NpcContext, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (!state.MoveTarget.HasValue)
            return NodeStatus.FAILURE;

        if(starting)
        {
            state.Me.NavMeshAgent.destination = state.MoveTarget.Value;
            if (AtDestination(state))
                return NodeStatus.SUCCESS;
        }

        if(state.Me.NavMeshAgent.path != null)
        {
            if (AtDestination(state))
                return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }

    bool AtDestination(NpcContext context)
    {
        // If we're already really close, don't bother pathing
        if (context.Me.NavMeshAgent.remainingDistance < 2.0f)
        {
            context.Me.LookAt(((IMoveContext)context).MoveTarget.Value);
            return true;
        }
        return false;
    }

    public override void OnReset()
    {
    }
}