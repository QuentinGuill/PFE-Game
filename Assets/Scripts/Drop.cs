using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private Item item;
    private int amount;

    public Item GetItem()
    {
        return item;
    }

    public int getAmount()
    {
        return amount;
    }

    public void setItem(Item item)
    {
        this.item = item;
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }
}
