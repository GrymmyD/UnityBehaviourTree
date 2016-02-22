# UnityBehaviourTree
A simple behaviour tree implementation for Unity.

Example usage:

    Node behaviourTree;
    Context behaviourState = new Context();

    void Start() {
        behaviourTree = CreateBehaviourTree();
        behaviourState = new Context();  // optionally add things you might need access to in your leaf nodes
    }

    void FixedUpdate() {
        behaviourTree.Behave(myBehaviourContext);
    }

    Node CreateBehaviourTree()
    {
        Sequence separate = new Sequence("separate",
            new TooCloseToEnemy(0.2f),
            new SetRandomDestination(),
            new Move());

        Sequence moveTowardsEnemy = new Sequence("moveTowardsEnemy",
            new HasEnemy(),
            new SetMoveTargetToEnemy(),
            new Inverter(new CanAttackEnemy()),
            new Inverter(new Succeeder(new Move())));

        Sequence attackEnemy = new Sequence("attackEnemy",
            new HasEnemy(),
            new CanAttackEnemy(),
            new StopMoving(),
            new AttackEnemy());

        Sequence needHeal = new Sequence("needHeal",
            new Inverter(new AmIHurt(15)),
            new AmIHurt(35),
            new FindClosestHeal(30),
            new Move());

        Selector chooseEnemy = new Selector("chooseEnemy",
            new TargetNemesis(),
            new TargetClosestEnemy(30));

        Sequence collectPowerup = new Sequence("collectPowerup",
            new FindClosestPowerup(50),
            new Move());

        Selector fightOrFlight = new Selector("fightOrFlight",
            new Inverter(new Succeeder(chooseEnemy)),
            separate,
            needHeal,
            moveTowardsEnemy,
            attackEnemy);

        Repeater repeater = new Repeater(fightOrFlight);

        return repeater;
    }


NOTE: The leaf nodes provided are tied to my specific implementation and you'll need to customize them
to suit your own needs - but this should be rather trivial.