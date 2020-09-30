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
    public static Node Wander()
    {
        return new Repeater(
            new Sequence("Wander",
                new SetRandomDestination(10f),
                new Move(),
                new Wait(3f)
            )
        );
    }

    public static Node MoveAndHarvest()
    {
        var holder = new Selector("HavestXorMove",
            HarvestResource(),
            Wander());
        return new RepeatUntilFail(holder);
    }

    public static Node HarvestResource()
    {
        return new Sequence("HarvestResource",
            MoveToResource(),
            new BreakResource()
        );
    }

    public static Node MoveToResource()
    {
        return new Sequence("MoveToResource",
            new TargetClosestResource(15f),
            new SetMoveTargetToResource(),
            new Move());
    }
}
