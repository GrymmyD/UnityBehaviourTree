using UnityEngine;
using System.Collections;
using System;

public class Repeater<T> : Decorator<T> where T: BehaviourState
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

public class RepeatUntilFail<T> : Decorator<T> where T: BehaviourState
{
    public RepeatUntilFail(Node<T> child) : base(child)
    {

    }

    public override NodeStatus OnBehave(T state)
    {
        NodeStatus ret = child.Behave(state);
        switch (ret)
        {
            case NodeStatus.FAILURE:
                return NodeStatus.SUCCESS;
            case NodeStatus.RUNNING:
            case NodeStatus.SUCCESS:
                Reset();
                child.Reset();
                return NodeStatus.RUNNING;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
