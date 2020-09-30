using UnityEngine;
using System.Collections;
using System;

public class SetRandomDestination : Leaf {
    public float wanderRange { get; set; }
    public SetRandomDestination(float wanderRange)
    {
        this.wanderRange = wanderRange;
    }
    public override NodeStatus OnBehave(BehaviourState state)
    {
        var context = (FriendlyNpcContext)state;
        context.moveTarget = context.me.transform.position + new Vector3(UnityEngine.Random.Range(-wanderRange, wanderRange), 0, UnityEngine.Random.Range(-wanderRange, wanderRange));
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }

}
