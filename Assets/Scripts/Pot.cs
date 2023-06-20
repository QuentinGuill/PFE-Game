using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IEntity
{
    private bool is_dead;

    public void die()
    {
        this.is_dead = true;
        lootItem();
    }

    private void lootItem()
    {
        GameObject drop = (GameObject)Instantiate(Resources.Load("Drop/Drop"), new Vector3(this.transform.position.x, this.transform.position.y, 0f), Quaternion.identity);
        drop.GetComponent<Drop>().setItem(new Shards());
        drop.GetComponent<Drop>().setAmount(3);
    }

    public bool isDead()
    {
        return is_dead;
    }

    public void takeDamage(int damage)
    {
        this.die();
    }
}
