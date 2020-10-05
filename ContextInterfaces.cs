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
