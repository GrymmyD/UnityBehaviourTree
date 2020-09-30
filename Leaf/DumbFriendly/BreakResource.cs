using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakResource :Leaf 
{
    public float BreakRadius { get; set; }

    public BreakResource(float breakRadius = 10f)
    {
        BreakRadius = breakRadius;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        if (starting && context.ResourceNode == null)
            return NodeStatus.FAILURE;
        else if (context.ResourceNode == null)
        {
            context.ResourceNode = null;
            return NodeStatus.SUCCESS;
        }

        if (Vector3.Distance(context.me.transform.position, context.ResourceNode.transform.position) > BreakRadius)
            return NodeStatus.FAILURE;

        context.me.currentTarget = context.ResourceNode.gameObject;
        context.me.Animator.SetTrigger("Shoot");
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
