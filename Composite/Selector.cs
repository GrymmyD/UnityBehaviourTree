﻿using UnityEngine;
using System.Collections;
using System;

public class Selector<T> : Composite<T> where T: BehaviourState
{
    int currentChild = 0;

    public Selector(string compositeName, params Node<T>[] nodes) : base(compositeName, nodes)
    {

    }

    public override NodeStatus OnBehave(T state)
    {
        if(currentChild >= children.Count)
        {
            return NodeStatus.FAILURE;
        }

        NodeStatus ret = children[currentChild].Behave(state);

        switch(ret)
        {
            case NodeStatus.SUCCESS:
                return NodeStatus.SUCCESS;

            case NodeStatus.FAILURE:
                currentChild++;

                // If we failed, immediately process the next child
                return OnBehave(state);
        }
        return NodeStatus.RUNNING;
    }

    public override void OnReset()
    {
        currentChild = 0;
        foreach (Node<T> child in children)
        {
            child.Reset();
        }
    }
}
