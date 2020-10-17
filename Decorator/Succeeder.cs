using UnityEngine;
using System.Collections;
using System;
using SSG.BehaviourTrees.Primitives;

namespace SSG.BehaviourTrees.Decorators
{
    /// <summary>
    /// Always returns Success or Running
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Succeeder<T> : Decorator<T> where T : BehaviourState
    {
        public Succeeder(Node<T> child) : base(child)
        {

        }
        public override NodeStatus OnBehave(T state)
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

    public class Runner<T> : Node<T> where T : BehaviourState
    {
        public override NodeStatus OnBehave(T state)
        {
            return NodeStatus.RUNNING;
        }

        public override void OnReset()
        {
        }
    }
}
