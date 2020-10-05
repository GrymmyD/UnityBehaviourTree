using SSG.BehaviourTrees.Composites;
using SSG.BehaviourTrees.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class NpcLeafFactory
{
    public static Node<T> Wander<T>() where T : NpcContext, IMoveContext
    {
        return new Sequence<T>("Wander",
                new SetRandomDestination<T>(10f),
                new Move<T>(2f)
        );
    }

    public static Node<T> MoveAndHarvest<T>() where T: NpcContext, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext

    {

        return  new Interruptor<T>("test",
            Wander<T>(),
            HarvestResource<T>(),
            AttackIfHasEnemy<T>()
        );
    }

    public static Node<T> HarvestResource<T>() where T: NpcContext, IMoveContext, IHasResourceTarget, ILook,IHasEnemyContext
    {
        return new Sequence<T>("HarvestResource",
            MoveToResource<T>(),
            new HasTarget<T>(),
            new Attack<T>(15f),
            new ResetLookAt<T>()
        );
    }

    public static Node<T> MoveToResource<T>() where T :  NpcContext, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext
    {
        return new Sequence<T>("MoveToResource",
            new TargetClosestResource<T>(15f),
            new HasTarget<T>(),
            new SetMoveTargetToTarget<T>(),
            new Move<T>(4f),
            new SetLookAtTarget<T>(),
            new LookAt<T>(),
            new StopMove<T>());
    }
    public static Node<T> Patrol<T>() where T: NpcContext, IPatrolContext, IMoveContext,ILook
    {
        var patrolLeg = new Sequence<T>("PatrolPoints",
            new ResetLookAt<T>(),
            new SetMoveToNextPatrolPoint<T>(),
            new Move<T>(3f),
            new SetNextPatrolPoint<T>());
        return patrolLeg;
    }

    public static Node<T> DeathPatrol<T>() where T: NpcContext, IPatrolContext, IMoveContext, IHasEnemyContext,ILook,IHasResourceTarget
    {
        return new Interruptor<T>("DeathPatrol",
            Patrol<T>(),
            AttackIfHasEnemy<T>());
    }

    public static Node<T> AttackIfHasEnemy<T>() where T : NpcContext, IHasEnemyContext, ILook, IHasResourceTarget, IMoveContext
    {
        return new Sequence<T>("AttackIfHasEnemy",
            new TargetClosestEnemy<T>(15f),
            new HasTarget<T>(),
            new SetMoveTargetToTarget<T>(),
            new Move<T>(13f),
            new StopMove<T>(),
            new CanAttackTarget<T>(),
            new SetLookAtTarget<T>(),
            new LookAt<T>(),
            new Attack<T>(15f),
            new ResetLookAt<T>()

            ) ;
    }
}
