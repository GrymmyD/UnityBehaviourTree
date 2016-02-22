using UnityEngine;
using System.Collections;
using System;
using Pathfinding;

public class Move : Leaf
{
    Path currentPath = null;
    int waypoint = 0;

    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        if (!context.moveTarget.HasValue)
            return NodeStatus.FAILURE;

        if(starting)
        {
            if (AtDestination(context))
                return NodeStatus.SUCCESS;

            context.seeker.StartPath(context.me.transform.position, context.moveTarget.Value, (Path p) =>
            {
                currentPath = p;
                waypoint = 2;
            });
        }

        if(currentPath != null)
        {
            if (AtDestination(context))
                return NodeStatus.SUCCESS;

            // Only move for a maximum of 30 ticks (.5 seconds)
            if (ticks > 30)
            {
                context.me.LookAt(context.moveTarget.Value);
                return NodeStatus.SUCCESS;
            }

            if (waypoint >= currentPath.vectorPath.Count)
            {
                context.me.LookAt(context.moveTarget.Value);
                return NodeStatus.SUCCESS;
            }

            Vector3 pathPoint = currentPath.vectorPath[waypoint];
            if (context.me.DistanceTo(pathPoint) < 0.5f)
            {
                waypoint++;
            }
            else
            {
                context.me.LookAt(pathPoint);
                context.me.MoveForward(true);
            }
        }

        return NodeStatus.RUNNING;
    }

    bool AtDestination(Context context)
    {
        // If we're already really close, don't bother pathing
        if (context.me.DistanceTo(context.moveTarget.Value) < 2.0f)
        {
            context.me.LookAt(context.moveTarget.Value);
            return true;
        }
        return false;
    }

    public override void OnReset()
    {
        currentPath = null;
        waypoint = 0;
    }
}