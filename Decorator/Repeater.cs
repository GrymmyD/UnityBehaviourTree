using UnityEngine;
using System.Collections;
using System;

public class Repeater : Decorator
{
    public Repeater(Node child) : base(child)
    {

    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        NodeStatus ret = child.Behave(state);
        if (ret != NodeStatus.RUNNING)
        {
            Reset();
            child.Reset();
        }
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
