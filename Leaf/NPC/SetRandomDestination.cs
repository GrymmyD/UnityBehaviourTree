using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

public class SetRandomDestination <T>: Leaf <T> where T : NpcContext, IMoveContext
{
    public float wanderRange { get; set; }
    public SetRandomDestination(float wanderRange)
    {
        this.wanderRange = wanderRange;
    }
    public override NodeStatus OnBehave(T state)
    {
        state.MoveTarget = state.Me.transform.position + new Vector3(UnityEngine.Random.Range(-wanderRange, wanderRange), 0, UnityEngine.Random.Range(-wanderRange, wanderRange));
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }

}
