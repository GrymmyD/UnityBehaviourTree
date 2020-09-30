using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMoveTargetToResource : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        if (context.ResourceNode == null)
            return NodeStatus.FAILURE;

        context.moveTarget = context.ResourceNode.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
