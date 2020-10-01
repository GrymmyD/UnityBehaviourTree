using UnityEngine;
using System.Collections;

public class FriendlyNpcContext : NpcContext, IMoveContext, IHasEnemyContext, IHasResourceTarget
{
    public GameObject Enemy { get; set; }
    public ResourceNode ResourceNode { get; set; }
    public Vector3? MoveTarget { get; set; }
}

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
