using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Entity
{
    public abstract void takeDamage(int damage);

    public abstract void die();

    public abstract bool isDead();
}
