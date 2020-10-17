using SSG.BehaviourTrees.Decorators;
using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NpcCommandTruthy<T> : Leaf<T> where T : BehaviourState, ICommandable
{
    /// <summary>
    /// Will return success when npc command matches the value in the constructor
    /// </summary>
    /// <param name="toCheckAgainst"></param>
    public NpcCommandTruthy(NpcCommands toCheckAgainst)
    {
        ToCheckAgainst = toCheckAgainst;
    }

    public NpcCommands ToCheckAgainst { get; set; }
    public override NodeStatus OnBehave(T state)
    {
        if (ToCheckAgainst == state.RecievedCommand)
            return NodeStatus.SUCCESS;
        return NodeStatus.FAILURE;

    }

    public override void OnReset()
    {
    }
}

public class IsAwaitingCommand<T> : Leaf<T> where T : BehaviourState, ICommandable
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.AwaitingCommand)
            return NodeStatus.SUCCESS;
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}

public class HasUpdatedCommand<T> : Leaf<T> where T : BehaviourState, ICommandable
{
    public override NodeStatus OnBehave(T state)
    {
        if (state.UpdatedCommand)
        {
            state.UpdatedCommand = false;
            return NodeStatus.SUCCESS;
        }
        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}

public class SetMoveTargetToMoveCommandTarget<T> : Leaf<T> where T : NpcContext, ICommandable, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.MoveTarget = state.MoveCommandTarget;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}

public class SetMoveTargetToDefendThingTarget<T> : Leaf<T> where T : NpcContext, ICommandable, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.MoveTarget = state.DefendThingCommandTarget.transform.position;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}

public class CleanMoveTargetState<T> : Leaf<T> where T : NpcContext, ICommandable, IMoveContext
{
    public override NodeStatus OnBehave(T state)
    {
        state.MoveCommandTarget = null;
        return NodeStatus.SUCCESS;
    }

    public override void OnReset()
    {
    }
}
