using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Image itemImage;

    Item item;

    public Item Item
    {
        get => item;
        set
        {
            item = value;
            if (item != null)
            {
                itemImage.gameObject.SetActive(true);
                itemImage.sprite = item.Sprite;
            }
            else
                itemImage.gameObject.SetActive(false);
        }
    }

}
