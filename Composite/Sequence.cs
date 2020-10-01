using UnityEngine;
using System.Collections;
using System;

public class Sequence<T> : Composite<T> where T : BehaviourState
{
    int currentChild = 0;

    public Sequence(string compositeName, params Node<T>[] nodes) : base(compositeName, nodes)
    {

    }

    public override NodeStatus OnBehave(T state)
    {
        NodeStatus ret = children[currentChild].Behave(state);

        switch(ret)
        {
            case NodeStatus.SUCCESS:
                currentChild++;
                break;

            case NodeStatus.FAILURE:
                return NodeStatus.FAILURE;
        }

        if (currentChild >= children.Count)
        {
            return NodeStatus.SUCCESS;
        } else if(ret == NodeStatus.SUCCESS)
        {
            // if we succeeded, don't wait for the next tick to process the next child
            return OnBehave(state);
        }

        return NodeStatus.RUNNING;
    }

    public override void OnReset()
    {
        currentChild = 0;
        foreach(Node<T> child in children)
        {
            child.Reset();
        }
    }
}
