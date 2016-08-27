using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
    public GameObject inventoryPanel;
    public GameObject item;
    public GameObject itemContainer;
    public ItemDatabase itemDatabase;

    public List<Item> itemList = new List<Item>();
    public List<GameObject> inventoryItems = new List<GameObject>();

    void Start()
    {
       // itemDatabase = GetComponent<ItemDatabase>();

       
       
        //item = inventoryPanel.transform.FindChild("Item").gameObject;

//        for (int i = 0; i < numberItems; i++)
//        {
//            itemList.Add(new Item());
//            inventoryItems.Add(Instantiate(item));
//
//            inventoryItems[i].transform.SetParent(inventoryPanel.transform);
//                inventoryItems[i].transform.localScale = new Vector3(1.0f, 1.0f);
//
//            /*
//            inventoryItems[i].transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = itemList[i].Description;
//            Debug.Log(inventoryItems[i].transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text);
//            */
//
//        }

        AddItem(0);
        AddItem(1);
    }

    void Awake()
    {
        
    }

    public void AddItem(int itemID)
    {
        Debug.Log("Item index" + itemID);
        Debug.Log(itemDatabase);
        Item itemToAdd = itemDatabase.FetchItemByID(itemID);

//        for (int i = 0; i < itemList.Count; i++)
//        {
//            if (itemList[i].ID == -1)
//            {
                //itemList[i] = itemToAdd;
                GameObject itemObj = Instantiate(item);
                itemObj.transform.SetParent(itemContainer.transform);
                itemObj.transform.position = Vector2.zero;
                itemObj.transform.localScale = Vector2.one;
        itemObj.GetComponent<testItem>().uiManager = GetComponent<UIManager>();
                itemObj.GetComponent<testItem>().ChangeInfo(itemToAdd, 2);

                /*
                inventoryItems[i].transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = itemToAdd.Description;
                Debug.Log(inventoryItems[i].transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text);
                */


               // break;
//            }
//        }
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
