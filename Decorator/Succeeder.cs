using UnityEngine;
using System.Collections;
using System;

public class Succeeder : Decorator
{
    public Succeeder(Node child) : base(child)
    {

    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        NodeStatus ret = child.Behave(state);

        if (ret == NodeStatus.RUNNING)
            return NodeStatus.RUNNING;

        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
