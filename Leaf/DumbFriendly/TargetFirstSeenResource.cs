using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Analytics;

class TargetFirstSeenResource : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;

        foreach (var item in context.me.seenTargets)
        {
            if (item == null)
                continue;
            var holder = item.GetComponent<ResourceNode>();
            if (holder != null)
            {
                context.ResourceNode = holder;
                return NodeStatus.SUCCESS;
            }
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
