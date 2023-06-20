using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item
{
    public Wood()
    {

    }

    public override string getName()
    {
        return "Wood";
    }

    public override int getStackSize()
    {
        return 99;
    }
}
