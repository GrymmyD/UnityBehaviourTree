using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNpcContext : NpcContext, IMoveContext, IHasEnemyContext, IHasResourceTarget, ILook, ICommandable
{
    public GameObject Enemy { get; set; }
    public ResourceNode ResourceNode { get; set; }
    public Vector3? MoveTarget { get; set; }
    public GameObject LookTarget { get; set; }
    public bool AwaitingCommand { get; set; }
    public NpcCommands RecievedCommand { get; set; }
    public Vector3? MoveCommandTarget { get; set; }
    public GameObject DefendThingCommandTarget { get; set; }
    public Vector3? DefendPlaceCommandTarget { get; set; }
    public bool IsRetreating { get; set; }
    public bool UpdatedCommand { get; set; }
}
