using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public GameObject inventoryPanel;
    public GameObject item;
    public ItemDatabase itemDatabase;
    int numberItems;

    public List<Item> itemList = new List<Item>();
    public List<GameObject> inventoryItems;

    void Start()
    {
        //itemDatabase = GetComponent<ItemDatabase>();

        numberItems = 5;
        inventoryPanel = GameObject.Find("Content");
        item = inventoryPanel.transform.FindChild("Item").gameObject;

        for (int i = 0; i < numberItems; i++)
        {
            itemList.Add(new Item());
            inventoryItems.Add(Instantiate(item));
            inventoryItems[i].transform.SetParent(inventoryPanel.transform);
            inventoryItems[i].transform.localScale = new Vector3(1.0f, 1.0f);
        }

        AddItem(0);
    }

    public void AddItem(int itemID)
    {
        Item itemToAdd = itemDatabase.FetchItemByID(itemID);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ID == -1)
            {
                itemList[i] = itemToAdd;
                GameObject itemObj = Instantiate(item);
                itemObj.transform.SetParent(inventoryItems[i].transform);
                itemObj.transform.position = Vector2.zero;
                break;
            }
        }
    }

    /*
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public GameObject ItemPanel;
   
    public List<IConsumeableItem> items = new List<IConsumeableItem>();
    public List<GameObject> slots = new List<GameObject>();

    //HACK - Marked for change:
    //We might make the Player class a Singleton later!
    public GameObject playerInfo;

    void Awake()
    {
        items = playerInfo.GetComponent<IPlayerDeployedConsumableItemInfo>().Deployed_Consumable_Items;
    }
        
    void OnEnable()
    {
        for(int i = 0; i < items.Count; i++)
        {
            GameObject slot = Instantiate(inventorySlot);
            slot.GetComponent<Item>().
        }

        //inventorySlot = GameObject.Find("Inventory Slot");
        //inventoryItem = inventorySlot.transform.FindChild("Inventory Item");
    }
    */
}
