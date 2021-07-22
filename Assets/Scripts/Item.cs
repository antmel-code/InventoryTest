using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    Sprite sprite;

    public Sprite Sprite
    {
        get => sprite;
    }
}
