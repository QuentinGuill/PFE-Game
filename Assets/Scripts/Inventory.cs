using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private struct InventorySlot
    {
        public int stack;
        public Item item;
    }
    private InventorySlot[,] inventory;
    private int ColumnLength;
    private int RowHeight;

    void Awake()
    {
        ColumnLength = 3;
        RowHeight = 3;
        inventory = new InventorySlot[ColumnLength, RowHeight];
    }

    private void FixedUpdate()
    {
        LayerMask mask = LayerMask.GetMask("Drops");
        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, 1f, Vector2.one, 0.0f, mask);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponentInParent<Drop>() != null)
            {
                Debug.Log("Item picked up!");
                int res = this.addItem(hit.collider.GetComponentInParent<Drop>().GetItem(), hit.collider.GetComponentInParent<Drop>().getAmount());
                if (res > 0)
                {
                    hit.collider.GetComponentInParent<Drop>().setAmount(res);
                }
                else
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    public Vector2 hasItem(string name)
    {
        Vector2 itemPos = new Vector2(-1, -1);

        for(int i = 0; i < ColumnLength-1; i++)
        {
            for (int j = 0; j < RowHeight - 1; j++)
            {
                if (slotContainsItem(inventory[i, j]))
                {
                    itemPos = new Vector2(i, j);
                }
            }
        }

        return itemPos;
    }

    private bool slotContainsItem(InventorySlot slot)
    {
        bool slotContainsItem = false;

        if (slot.item != null)
        {
            if (slot.item.getName() == name)
            {
                if (slot.stack < slot.item.getStackSize())
                {
                    slotContainsItem = true;
                }
            }
        }

        return slotContainsItem;
    }

    public Vector2 findFirstEmptySlot()
    {
        Vector2 firstSlot = new Vector2(-1, -1);

        for (int i = 0; i < ColumnLength - 1; i++)
        {
            for (int j = 0; j < RowHeight - 1; j++)
            {
                if (inventory[i,j].item == null)
                {
                    firstSlot = new Vector2(i, j);
                }
            }
        }

        return firstSlot;
    }

    public int addItem(Item item, int amount)
    {
        Vector2 itemPos = hasItem(item.getName());

        if (itemPos == new Vector2(-1,-1))
        {
            Vector2 firstSlot = findFirstEmptySlot();

            if(firstSlot == new Vector2(-1, -1))
            {
                return amount;
            } else
            {
                inventory[(int)firstSlot.x, (int)firstSlot.y].item = item;
                inventory[(int)firstSlot.x, (int)firstSlot.y].stack = amount;
                return 0;
            }
        }
        else
        {
            if(inventory[(int)itemPos.x, (int)itemPos.y].stack + amount > item.getStackSize())
            {
                inventory[(int)itemPos.x, (int)itemPos.y].stack = item.getStackSize();
                return addItem(item, inventory[(int)itemPos.x, (int)itemPos.y].stack + amount - item.getStackSize());
            }
            else
            {
                inventory[(int)itemPos.x, (int)itemPos.y].stack += amount;
                return 0;
            }
        }
    }

    public bool removeItem(int i, int j)
    {
        if (inventory[i,j].item == null)
        {
            return false;
        }
        else
        {
            inventory[i, j].item = null;
            inventory[i, j].stack = 0;
            return true;
        }
    }
}
