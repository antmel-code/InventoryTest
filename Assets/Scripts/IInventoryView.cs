using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryView
{
    void UpdateView(Item[] inventory, Item[] bag);
    void UpdateSelectedItem(int selectedItemIndex, bool inBag);
}
