using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyContext : NpcContext, IPatrolContext, IMoveContext, IHasEnemyContext, ILook, IHasResourceTarget
{
    public List<Vector3> PatrolPoints { get; set; } = new List<Vector3>();
    public int PatrolIndex { get; set; }
    public Vector3? MoveTarget { get; set; }
    public GameObject Enemy { get; set; }
    public GameObject LookTarget { get; set; }
    public ResourceNode ResourceNode { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
