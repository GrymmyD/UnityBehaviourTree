using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Leaf
{
    public float WaitTimeInSeconds { get; private set; }
    private float tracker;
    public Wait(float waitTimeInSeconds)
    {
        WaitTimeInSeconds = waitTimeInSeconds;
        tracker = WaitTimeInSeconds;
    }

    public override NodeStatus OnBehave(BehaviourState state)
    {
        tracker -= Time.deltaTime;
        if (tracker > 0)
            return NodeStatus.RUNNING;
        else
            return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
        tracker = WaitTimeInSeconds;
    }
}
