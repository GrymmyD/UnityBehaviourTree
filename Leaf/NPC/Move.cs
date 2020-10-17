using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class Move<T> : Leaf<T> where T: NpcContext, IMoveContext
{
    public float AcceptableDistance { get; set; }

    public Move(float acceptableDistance = 1f)
    {
        AcceptableDistance = acceptableDistance;
    }

    public override NodeStatus OnBehave(T state)
    {
        if (!state.MoveTarget.HasValue)
            return NodeStatus.FAILURE;

        if(starting)
        {
            state.Me.NavMeshAgent.isStopped = false;
            state.Me.NavMeshAgent.destination = state.MoveTarget.Value;
            if (AtDestination(state))
                return NodeStatus.SUCCESS;
        }

        if(AtDestination(state)) 
        {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.RUNNING;
    }

    bool AtDestination(NpcContext context)
    {
        // If we're already really close, don't bother pathing
        if (Vector3.Distance(context.Me.transform.position, ((IMoveContext)context).MoveTarget.Value) <= AcceptableDistance)
        {
            return true;
        }
        return false;
    }

    public override void OnReset()
    {
    }
}

public class StopMove<T> : Leaf<T> where T : NpcContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.Me.NavMeshAgent.isStopped = true;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
