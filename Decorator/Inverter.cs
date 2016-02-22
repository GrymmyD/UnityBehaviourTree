using UnityEngine;
using System.Collections;
using System;

public class Inverter : Decorator
{
    public Inverter(Node child) : base(child)
    {

    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        switch(child.Behave(state))
        {
            case NodeStatus.RUNNING:
                return NodeStatus.RUNNING;

            case NodeStatus.SUCCESS:
                return NodeStatus.FAILURE;

            case NodeStatus.FAILURE:
                return NodeStatus.SUCCESS;
        }

        Debug.Log("SHOULD NOT GET HERE");
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
