using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private List<Text> slots;
    [SerializeField]
    private Text emeraldsAmount;

    private GetUserInventoryResult PFInventory;

    void Awake()
    {
        ColumnLength = 3;
        RowHeight = 3;
        inventory = new InventorySlot[ColumnLength, RowHeight];
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnInventoryUpdateSuccess, OnInventoryUpdateError);
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
                int res = this.addItem(hit.collider.GetComponentInParent<Drop>().GetItem(), hit.collider.GetComponentInParent<Drop>().getAmount());
                if (res > 0)
                {
                    hit.collider.GetComponentInParent<Drop>().setAmount(res);
                    PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnInventoryUpdateSuccess, OnInventoryUpdateError);
                }
                else
                {
                    Destroy(hit.transform.gameObject);
                }
                this.updateInventoryDisplay();
            }
        }
    }

    private void OnInventoryUpdateError(PlayFabError result)
    {
        Debug.Log("error: " + result);
    }

    private void OnInventoryUpdateSuccess(GetUserInventoryResult result)
    {
        this.PFInventory = result;
    }

    public Vector2 hasItem(string name)
    {
        Vector2 itemPos = new Vector2(-1, -1);

        for(int i = 0; i < ColumnLength; i++)
        {
            for (int j = 0; j < RowHeight; j++)
            {
                if (slotContainsItem(inventory[i, j], name))
                {
                    itemPos = new Vector2(i, j);
                }
            }
        }

        return itemPos;
    }

    private bool slotContainsItem(InventorySlot slot, string name)
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
        for (int i = 0; i < ColumnLength; i++)
        {
            for (int j = 0; j < RowHeight; j++)
            {
                if (inventory[i,j].item == null)
                {
                    Debug.Log("The first empty slot is " + i + ", " + j);
                    return (new Vector2(i, j));
                }
            }
        }

        return (new Vector2(-1, -1));
    }

    public int addItem(Item item, int amount)
    {
        Vector2 itemPos = hasItem(item.getName());
        Debug.Log(item.getName());

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

    private void updateInventoryDisplay()
    {
        emeraldsAmount.text = PFInventory.VirtualCurrency["EM"].ToString();
        for (int i = 0; i < ColumnLength; i++)
        {
            for (int j = 0; j < RowHeight; j++)
            {
                if (inventory[i, j].item == null)
                {
                    slots[i * 3 + j].text = "Empty";
                }
                else
                {
                    slots[i * 3 + j].text = inventory[i, j].item.getName() + " x" + inventory[i, j].stack;
                }
            }
        }
    }
}
