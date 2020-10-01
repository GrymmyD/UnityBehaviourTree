using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Analytics;

class TargetFirstSeenResource <T>: Leaf<T> where T :NpcContext,IHasResourceTarget
{
    public override NodeStatus OnBehave(T state)
    {
        foreach (var item in ((IHasTargetList)state.Me).seenTargets)
        {
            if (item == null)
                continue;
            var holder = item.GetComponent<ResourceNode>();
            if (holder != null)
            {
                state.ResourceNode = holder;
                return NodeStatus.SUCCESS;
            }
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
