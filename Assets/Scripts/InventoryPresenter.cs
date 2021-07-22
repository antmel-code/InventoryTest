using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField]
    InventoryView inventoryView;

    [SerializeField]
    Item[] startItems;

    int selectedItemIndex = -1;

    /// <summary>
    /// Определяет то, где выделен объект
    /// </summary>
    bool inBag = false;

    int firstBagFreeSlot = -1;
    int firstInventoryFreeSlot = -1;

    Item SelectedItem
    {
        get
        {
            if (selectedItemIndex < 0)
                return null;
            else
                return inBag ? model.bag[selectedItemIndex] : model.inventory[selectedItemIndex];
        }
    }

    InventoryModel model;
    public void SelectItem(int itemIndex, bool inBag = false)
    {
        if (itemIndex == selectedItemIndex && inBag == this.inBag)
        {
            selectedItemIndex = -1;
        }
        else
        {
            this.inBag = inBag;
            selectedItemIndex = itemIndex;
        }
        inventoryView.UpdateSelectedItem(selectedItemIndex, inBag);
        inventoryView.UpdateMoveButton(SelectedItem != null);
    }

    public void SelectItemInInventory(int itemIndex)
    {
        SelectItem(itemIndex, false);
    }

    public void SelectItemInBag(int itemIndex)
    {
        SelectItem(itemIndex, true);
    }

    public void MoveSelectedItem()
    {
        MoveItem(selectedItemIndex, inBag);
        inventoryView.UpdateView(model.inventory, model.bag);
        inventoryView.UpdateMoveButton(false);
    }

    public bool MoveItem(int itemIndex, bool inBag = false)
    {
        if (inBag)
        {
            return MoveItemToInventory(itemIndex);
        }
        else
        {
            return MoveItemToBag(itemIndex);
        }
    }

    public bool MoveItemToBag(int itemIndex)
    {
        if (selectedItemIndex < 0 || selectedItemIndex >= model.maxInventorySize || !IsFreeSpaceInBag() || model.inventory[itemIndex] == null)
        {
            return false;
        }
        model.bag[FindFirstBagFreeSlot()] = model.inventory[selectedItemIndex];
        firstBagFreeSlot = -1;
        model.inventory[selectedItemIndex] = null;
        if (selectedItemIndex < firstInventoryFreeSlot)
        {
            firstInventoryFreeSlot = -1;
        }
        return true;
    }

    public bool MoveItemToInventory(int itemIndex)
    {
        if (selectedItemIndex < 0 || selectedItemIndex >= model.maxInventorySize || !IsFreeSpaceInInventory() || model.bag[itemIndex] == null)
        {
            return false;
        }
        model.inventory[FindFirstInventoryFreeSlot()] = model.bag[selectedItemIndex];
        firstInventoryFreeSlot = -1;
        model.bag[selectedItemIndex] = null;
        if (selectedItemIndex < firstBagFreeSlot)
        {
            firstBagFreeSlot = -1;
        }
        return true;
    }

    public bool IsFreeSpaceInBag()
    {
        return FindFirstBagFreeSlot() != -1;
    }

    public bool IsFreeSpaceInInventory()
    {
        return FindFirstInventoryFreeSlot() != -1;
    }

    int FindFirstInventoryFreeSlot()
    {
        if (firstInventoryFreeSlot != -1)
            return firstInventoryFreeSlot;
        for (int index = 0; index < model.inventory.Length; index++)
        {
            var item = model.inventory[index];
            if (item == null)
            {
                firstInventoryFreeSlot = index;
                return firstInventoryFreeSlot;
            }
        }
        firstInventoryFreeSlot = -1;
        return firstInventoryFreeSlot;
    }

    int FindFirstBagFreeSlot()
    {
        if (firstBagFreeSlot != -1)
            return firstBagFreeSlot;
        for (int index = 0; index < model.bag.Length; index++)
        {
            var item = model.bag[index];
            if (item == null)
            {
                firstBagFreeSlot = index;
                return firstBagFreeSlot;
            }
        }
        firstBagFreeSlot = -1;
        return firstBagFreeSlot;
    }

    private void Awake()
    {
        model = InventoryModel.Instance;
        InitWithSomeItems();
    }

    private void InitWithSomeItems()
    {
        for (int index = 0; index < startItems.Length && index < model.maxInventorySize; index++)
        {
            model.inventory[index] = startItems[index];
        }
        inventoryView.UpdateView(model.inventory, model.bag);
    }

}
