using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : Singleton<InventoryModel>
{
    public Item[] inventory;
    public Item[] bag;
    public int maxInventorySize = 25;
    public int maxBagSize = 25;

    protected override void Init()
    {
        inventory = new Item[maxInventorySize];
        bag = new Item[maxBagSize];
    }
}
