using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DumbFriendlyLeafFactory
{
    //public static Node GetCollectBehavior(dynamic inputs)
    //{
    //    return new Sequence("CollectionBehavior", 
    //        new Selector("MoveOr",new Move()))
    //}
    public static Node<T> Wander<T>() where T: NpcContext, IMoveContext
    {
        return new Repeater<T>(
            new Sequence<T>("Wander",
                new SetRandomDestination<T>(10f),
                new Move<T>(),
                new Wait<T>(3f)
            )
        );
    }

    public static Node<T> MoveAndHarvest<T>() where T: NpcContext, IMoveContext, IHasResourceTarget

    {
        var holder = new Selector<T>("HavestXorMove",
            HarvestResource<T>(),
            Wander<T>());
        return new RepeatUntilFail<T>(holder);
    }

    public static Node<T> HarvestResource<T>() where T: NpcContext, IMoveContext, IHasResourceTarget

    {
        return new Sequence<T>("HarvestResource",
            MoveToResource<T>(),
            new BreakResource<T>()
        );
    }

    public static Node<T> MoveToResource<T>() where T :  NpcContext, IMoveContext, IHasResourceTarget
    {
        return new Sequence<T>("MoveToResource",
            new TargetClosestResource<T>(15f),
            new SetMoveTargetToResource<T>(),
            new Move<T>());
    }
}
