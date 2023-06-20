using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shards : Item
{
    public Shards()
    {

    }

    public override string getName()
    {
        return "Shard";
    }

    public override int getStackSize()
    {
        return 99;
    }
}
