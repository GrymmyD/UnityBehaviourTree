using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UnityEngine;

class LookAt<T> : Node<T> where T : NpcContext,ILook
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.LookTarget == null)
            return NodeStatus.FAILURE;

        var rotation = Quaternion.LookRotation(state.LookTarget.transform.position - state.Me.transform.position, state.Me.transform.up);
        if (Quaternion.Angle(state.Me.transform.rotation, rotation) < 25f) 
            return NodeStatus.SUCCESS;
        else
        {
            state.Me.LookAt(rotation);
            return NodeStatus.RUNNING;
        }
    }


    public override void OnReset()
    {
    }
}

class SetLookAtTarget<T> : Node<T> where T : NpcContext, ILook, IHasResourceTarget,IHasEnemyContext
{
    public override NodeStatus OnBehave(T state)
    {
        if (((IHasTarget)state.Me).currentTarget != null)
        {
            state.LookTarget = ((IHasTarget)state.Me).currentTarget;
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}

class ResetLookAt<T> : Node<T> where T: NpcContext, ILook
{
    public override NodeStatus OnBehave(T state)
    {
        if (Quaternion.Angle(state.Me.transform.rotation,Quaternion.LookRotation(state.Me.transform.forward)) < 5f)
        {
            state.Me.LookAt(null);
            return NodeStatus.SUCCESS;
        }

        state.LookTarget = null;
        state.Me.LookAt(Quaternion.LookRotation(state.Me.transform.forward));
        return NodeStatus.RUNNING; 
    }


    public override void OnReset()
    {
    }
}
