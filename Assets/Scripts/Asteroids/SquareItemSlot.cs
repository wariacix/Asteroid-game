using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareItemSlot : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item
    {
        get { return item; }
        set { item = value; }
    }
}
