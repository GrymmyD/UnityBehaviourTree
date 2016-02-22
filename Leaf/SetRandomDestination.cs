using UnityEngine;
using System.Collections;
using System;

public class SetRandomDestination : Leaf {

    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;
        context.moveTarget = new Vector3(UnityEngine.Random.Range(-1024, 1024), 0, UnityEngine.Random.Range(-1024, 1024));
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }

}
