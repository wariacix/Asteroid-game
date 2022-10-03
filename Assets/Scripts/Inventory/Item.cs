using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Ore,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/New Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
        set { sprite = value; }
    }

    [SerializeField]
    private ItemType type;
    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField]
    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    [SerializeField]
    private string description = "null";
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public Item(Item item)
    {
        sprite = item.Sprite;
        type = item.Type;
        description = item.Description;
        id = item.Id;
    }
}
