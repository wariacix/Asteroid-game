using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    private List<ItemSlot> itemList;

    [SerializeField]
    private int sizeX, sizeY;
    [SerializeField]
    private GameObject slotParent, itemParent, numberParent;

    [SerializeField]
    private Image newSlotImg, newItemImg;
    [SerializeField]
    private TextMeshProUGUI newNumberImg;

    private Image[] slotImg, itemImg;
    private TextMeshProUGUI[] numberImg;


    public Inventory(int _sizeX = 8, int _sizeY = 5)
    {
        sizeX = _sizeX;
        sizeY = _sizeY;
    }

    private void Awake()
    {
        slotImg = new Image[sizeX * sizeY];
        itemImg = new Image[sizeX * sizeY];
        numberImg = new TextMeshProUGUI[sizeX * sizeY];
    }

    private void Start()
    {
        itemList = new List<ItemSlot>();
        Initialize();
    }

    private void Initialize()
    {
        // Create slot grid objects //
        int a = 0;
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Image transferImg = Instantiate(newSlotImg);
                transferImg.transform.parent = slotParent.transform;
                slotImg[a] = transferImg;

                transferImg = Instantiate(newItemImg);
                transferImg.transform.parent = itemParent.transform;
                itemImg[a] = transferImg;

                TextMeshProUGUI transferText = Instantiate(newNumberImg);
                transferText.transform.parent = numberParent.transform;
                transferText.SetText("");
                numberImg[a] = transferText;
                a++;
            }
        }

        // Set item grid objects to item sprites //
        a = 0;
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if (a < itemList.Count)
                {
                    itemImg[a].sprite = itemList[a].Item.Sprite;
                    a++;
                }
            }
        }
    }

    public void AddItem(Item item, int amount)
    {
        bool isThereSimilarItem = false;

        for (int x = 0; x < itemList.Count; x++)
        {
            if (itemList[x].Item.Id == item.Id)
            {
                itemList[x].Amount += 1;
                numberImg[x].SetText(itemList[x].Amount.ToString());
                isThereSimilarItem = true;
            }
        }


        if (isThereSimilarItem == false)
        {
            ItemSlot newItem = new ItemSlot(item, amount);
            itemList.Add(newItem);
            int i = itemList.Count - 1;

            if (i < itemImg.Length)
            {
                itemImg[i].sprite = newItem.Item.Sprite;
                numberImg[i].SetText(itemList[i].Amount.ToString());
            }
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        bool isThereSimilarItem = false;

        for (int x = 0; x < itemList.Count; x++)
        {
            if (item == itemList[x].Item)
            {
                if (itemList[x].Amount > amount)
                {
                    itemList[x].Amount -= amount;
                }
                else
                {
                    itemList.Remove(itemList[x]);
                }
            }
        }
    }
}

public class ItemSlot
{
    private Item item;
    public Item Item
    {
        get { return item; }
        set { item = value; }
    }

    private int amount = 0;
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public ItemSlot(Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
}