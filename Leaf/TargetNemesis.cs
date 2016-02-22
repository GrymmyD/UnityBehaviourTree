using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class TargetNemesis : Leaf
{
    public override NodeStatus OnBehave(BehaviourState state)
    {
        Context context = (Context)state;

        List<KeyValuePair<NetworkInstanceId, int>> myList = context.me.GetComponent<Living_Combat>().damageHistory.ToList();
        myList.Sort((first, second) =>
        {
            return first.Value.CompareTo(second.Value);
        }
        );

        foreach(var potentialNemesis in myList)
        {
            if (potentialNemesis.Value >= 0 || potentialNemesis.Key == context.me.netId)
                continue;

            Living potential = Utils.LivingForNetObj(potentialNemesis.Key);

            if (!potential)
                continue;

            context.enemy = potential;
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.FAILURE;
    }

    public override void OnReset()
    {
    }
}
