using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyNpcContext : NpcContext, IMoveContext, IHasEnemyContext, IHasResourceTarget, ILook
{
    public GameObject Enemy { get; set; }
    public ResourceNode ResourceNode { get; set; }
    public Vector3? MoveTarget { get; set; }
    public GameObject LookTarget { get; set; }
}
