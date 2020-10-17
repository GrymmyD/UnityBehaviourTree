using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IMoveContext
{
    Vector3? MoveTarget { get; set; }
}

public interface IHasEnemyContext
{
    GameObject Enemy { get; set; }
}

public interface IHasResourceTarget
{
    ResourceNode ResourceNode { get; set; }
}

public interface IPatrolContext
{
    List<Vector3> PatrolPoints { get; set; }
    int PatrolIndex { get; set; }
}

public interface ILook
{
    GameObject LookTarget { get; set; }
}

public interface ICommandable
{
    bool AwaitingCommand { get; set; }
    bool UpdatedCommand { get; set; }
    NpcCommands RecievedCommand { get; set; }
    Vector3? MoveCommandTarget { get; set; }
    GameObject DefendThingCommandTarget { get; set; }
    Vector3? DefendPlaceCommandTarget { get; set; }
    bool IsRetreating { get; set; }
}
