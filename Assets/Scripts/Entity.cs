using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public void takeDamage(int damage);

    public void die();

    public bool isDead();
}
