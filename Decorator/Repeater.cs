using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

namespace SSG.BehaviourTrees.Decorators
{

    /// <summary>
    /// Resets child notes after execution unless child returns Running
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repeater<T> : Decorator<T> where T : BehaviourState
    {
        public Repeater(Node<T> child) : base(child)
        {

        }
        public override NodeStatus OnBehave(T state)
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
}
