using UnityEngine;
using System.Collections;

public class FriendlyNpcContext : BehaviourState, IMoveContext,Itest
{
    public GameObject enemy { get; set; }
    public DumbFriendly me { get; set; }
    public ResourceNode ResourceNode { get; set; }
    public Vector3? MoveTarget { get; set; }
}

public interface IMoveContext
{
    Vector3? MoveTarget { get; }
}
public interface Itest
{
    Vector3? MoveTarget { get; }
}
