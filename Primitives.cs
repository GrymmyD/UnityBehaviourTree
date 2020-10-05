using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

namespace SSG.BehaviourTrees.Primitives
{
    public enum NodeStatus
    {
        FAILURE,
        SUCCESS,
        RUNNING
    }

    public abstract class BehaviourState
    {
    }

    public abstract class Node<T> where T : BehaviourState
    {
        public Node()
        {
            //debug = true;
        }
        public bool starting = true;
        protected bool debug = false;
        public int ticks = 0;
        public static List<string> debugTypeBlacklist = new List<string>() { "Selector`1", "Sequence`1", "Repeater`1", "Inverter`1", "Succeeder`1", "RepeatAlways`1" };
        public virtual NodeStatus Behave(BehaviourState state)
        {
            NodeStatus ret = OnBehave((T)state);

            if (debug && !debugTypeBlacklist.Contains(GetType().Name))
            {
                string result = "Unknown";
                switch (ret)
                {
                    case NodeStatus.SUCCESS:
                        result = "success";
                        break;
                    case NodeStatus.FAILURE:
                        result = "failure";
                        break;

                    case NodeStatus.RUNNING:
                        result = "running";
                        break;
                }
                Debug.Log("Behaving: " + GetType().Name + " - " + result);
            }

            ticks++;
            starting = false;

            if (ret != NodeStatus.RUNNING)
                Reset();

            return ret;
        }

        public abstract NodeStatus OnBehave(T state);
        public void Reset()
        {
            starting = true;
            ticks = 0;
            OnReset();
        }

        public abstract void OnReset();
    }

    public abstract class Composite<T> : Node<T> where T : BehaviourState
    {
        protected List<Node<T>> children = new List<Node<T>>();
        public string compositeName;

        public Composite(string name, params Node<T>[] nodes)
        {
            compositeName = name;
            children.AddRange(nodes);
        }

        public override NodeStatus Behave(BehaviourState state)
        {
            bool shouldLog = debug && ticks == 0;
            if (shouldLog)
                Debug.Log("Running behaviour list: " + compositeName);

            NodeStatus ret = base.Behave(state);

            if (debug && ret != NodeStatus.RUNNING)
                Debug.Log("Behaviour list " + compositeName + " returned: " + ret.ToString());

            return ret;
        }
    }

    public abstract class Leaf<T> : Node<T> where T : BehaviourState
    {
    }

    public abstract class Decorator<T> : Node<T> where T : BehaviourState
    {
        protected Node<T> child;

        public Decorator(Node<T> node)
        {
            child = node;
        }
    }

}
