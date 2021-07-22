using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour, IInventoryView
{
    [SerializeField]
    ItemSlot[] bagSlots;

    [SerializeField]
    ItemSlot[] inventorySlots;

    [SerializeField]
    Image selectorOverlay;

    [SerializeField]
    Button moveButton;

    public void UpdateSelectedItem(int selectedItemIndex, bool inBag)
    {
        if (selectedItemIndex < 0)
        {
            selectorOverlay.gameObject.SetActive(false);
        }
        else
        {
            selectorOverlay.gameObject.SetActive(true);
            if (inBag)
            {
                selectorOverlay.rectTransform.SetParent(bagSlots[selectedItemIndex].transform, false);
            }
            else
            {
                selectorOverlay.rectTransform.SetParent(inventorySlots[selectedItemIndex].transform, false);
            }
        }
    }

    public void UpdateMoveButton(bool isEnabled)
    {
        moveButton.interactable = isEnabled;
    }

    public void UpdateView(Item[] inventory, Item[] bag)
    {
        for (int index = 0; index < inventorySlots.Length; index++)
        {
            inventorySlots[index].Item = inventory[index];
        }
        for (int index = 0; index < bagSlots.Length; index++)
        {
            bagSlots[index].Item = bag[index];
        }
    }

    
}
