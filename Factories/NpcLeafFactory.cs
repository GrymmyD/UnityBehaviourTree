using SSG.BehaviourTrees.Composites;
using SSG.BehaviourTrees.Decorators;
using SSG.BehaviourTrees.Primitives;
using System.Runtime.InteropServices.ComTypes;

public static class NpcLeafFactory
{
    public static Node<T> test<T>() where T : NpcContext, ICommandable, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext
    {
        var stopMoving = new Sequence<T>("Stop Moving if given command",
                new IsAwaitingCommand<T>(),
                new StopMove<T>()
            );

        var holder = new Interruptor<T>("MasterInterruptor",
                new Runner<T>(),
                MoveCommand<T>(),
                HarvestCommand<T>(),
                RetreatCommand<T>(),
                DefendThing<T>(),
                DefendPlace<T>(),
                stopMoving,
                new HasUpdatedCommand<T>()
            );

        return holder;
    }

    public static Node<T> MoveCommand<T>() where T : NpcContext, ICommandable, IMoveContext,ILook,IHasEnemyContext,IHasResourceTarget
    {
        var holder = new Sequence<T>("Move Command",
            new NpcCommandTruthy<T>(NpcCommands.MOVE),
            new Interruptor<T>("Break move for enemy",
                new Sequence<T>("Move Body",
                    new SetMoveTargetToMoveCommandTarget<T>(),
                    new Move<T>(5f),
                    new CleanMoveTargetState<T>()
                ),
                AttackIfHasEnemy<T>()
            )
        );
        return holder;
    }

    public static Node<T> HarvestCommand<T>() where T: NpcContext, ICommandable, IMoveContext, ILook, IHasEnemyContext, IHasResourceTarget
    {
        var holder = new Sequence<T>("Harvest Command",
            new NpcCommandTruthy<T>(NpcCommands.HARVEST),
            new Interruptor<T>("Break harvest for enemy",
                new Selector<T>("harvest Body",
                    new Sequence<T>("Harvest Move",
                        new SetMoveTargetToMoveCommandTarget<T>(),
                        new Move<T>(5f),
                        new CleanMoveTargetState<T>()
                    ),
                    HarvestResource<T>()
                ),
                AttackIfHasEnemy<T>()
            )
        );
        return holder;
    }

    public static Node<T> DefendPlace<T>() where T: NpcContext, ICommandable, IMoveContext, ILook, IHasEnemyContext, IHasResourceTarget
    {
        var holder = new Sequence<T>("Defend Location Command",
            new NpcCommandTruthy<T>(NpcCommands.DEFENDLOCATION),
            new Interruptor<T>("Break defence for new command",
                new Selector<T>("Defend Location Body",
                    new Sequence<T>("Defend place Move",
                        new SetMoveTargetToMoveCommandTarget<T>(),
                        new Move<T>(5f)
                    )
                ),
                AttackIfHasEnemy<T>()
            )
        );
        return holder;
    }

    public static Node<T> DefendThing<T>() where T: NpcContext, ICommandable, IMoveContext, ILook, IHasEnemyContext, IHasResourceTarget
    {
        var holder = new Sequence<T>("Defend Thing Command",
            new NpcCommandTruthy<T>(NpcCommands.DEFENDTHING),
            new Interruptor<T>("Break defence for new command",
                new Selector<T>("Defend thing Body",
                    new Sequence<T>("Defend thing Move",
                        new SetMoveTargetToDefendThingTarget<T>(),
                        new Move<T>(8f)
                    )
                ),
                AttackIfHasEnemy<T>()
            )
        );
        return holder;
    }

    public static Node<T> RetreatCommand<T>() where T: NpcContext, ICommandable, IMoveContext, ILook,  IHasEnemyContext, IHasResourceTarget
    {
        var holder = new Sequence<T>("Retreat",
            new NpcCommandTruthy<T>(NpcCommands.RETREAT),
            new Interruptor<T>("Break Retreat for new command",
                new Sequence<T>("Retreat Body",
                    new SetMoveTargetToMoveCommandTarget<T>(),
                    new Move<T>(5f),
                    new CleanMoveTargetState<T>()
                ),
                new HasUpdatedCommand<T>()
            )
        );
        return holder;
    }

    public static Node<T> Wander<T>() where T : NpcContext, IMoveContext
    {
        return new Sequence<T>("Wander",
                new SetRandomDestination<T>(10f),
                new Move<T>(2f)
        );
    }

    public static Node<T> MoveAndHarvest<T>() where T : NpcContext, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext

    {
        return new Interruptor<T>("test",
            Wander<T>(),
            HarvestResource<T>(),
            AttackIfHasEnemy<T>()
        );
    }

    public static Node<T> HarvestResource<T>() where T : NpcContext, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext
    {
        return new Sequence<T>("HarvestResource",
            MoveToResource<T>(),
            new HasTarget<T>(),
            new Attack<T>(15f),
            new ResetLookAt<T>()
        );
    }

    public static Node<T> MoveToResource<T>() where T : NpcContext, IMoveContext, IHasResourceTarget, ILook, IHasEnemyContext
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

    public static Node<T> Patrol<T>() where T : NpcContext, IPatrolContext, IMoveContext, ILook
    {
        var patrolLeg = new Sequence<T>("PatrolPoints",
            new ResetLookAt<T>(),
            new SetMoveToNextPatrolPoint<T>(),
            new Move<T>(3f),
            new SetNextPatrolPoint<T>());
        return patrolLeg;
    }

    public static Node<T> DeathPatrol<T>() where T : NpcContext, IPatrolContext, IMoveContext, IHasEnemyContext, ILook, IHasResourceTarget
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
            new SetLookAtTarget<T>(),
            new LookAt<T>(),
            new CanAttackTarget<T>(),
            new Attack<T>(15f),
            new ResetLookAt<T>()

            );
    }
}