using UnityEngine;
using System.Collections;

public class Context : BehaviourState
{
    public Bot me;
    public Seeker seeker;
    public Living enemy = null;
    public Vector3? moveTarget = null;
}
