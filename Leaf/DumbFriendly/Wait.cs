using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait<T> : Leaf<T> where T: BehaviourState
{
    public float WaitTimeInSeconds { get; private set; }
    private float tracker;
    public Wait(float waitTimeInSeconds)
    {
        WaitTimeInSeconds = waitTimeInSeconds;
        tracker = WaitTimeInSeconds;
    }

    public override NodeStatus OnBehave(T state)
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
