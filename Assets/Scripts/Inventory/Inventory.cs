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
    public GlobalInventory globalItemList;

    [SerializeField]
    private bool isCombatMode = false;

    public bool Is_Combat_Mode 
    {
        get { return isCombatMode; }
    }

    void Start()
    {
        if (((DummyGameManager)(SystemLocator.Instance.GetService(ObjectTypes.SystemDataType.GAMEMANAGER))).Game_State == ObjectTypes.GameStateType.COMBATMODE)
        {
            isCombatMode = true;
        }
        
        foreach (GlobalItem item in globalItemList._GlobalItemList)
        {
            AddItem(item.ID, item._ItemQuantity);
        }
    }

    public void AddItem(int itemID, int itemQuantity)
    {
        /*
        Debug.Log("Item index" + itemID);
        Debug.Log(itemDatabase);
        */
        Item itemToAdd = itemDatabase.FetchItemByID(itemID);

        GameObject itemObj = Instantiate(item);
        itemObj.transform.SetParent(itemContainer.transform);
        itemObj.transform.position = Vector2.zero;
        itemObj.transform.localScale = Vector2.one;

        itemObj.GetComponent<UpdateItemUI>().ChangeInfo(itemToAdd, itemQuantity, GetComponent<InventoryUIManager>());

        if (isCombatMode)
        {
            itemObj.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public void RemoveItem(int itemID, int itemQuantity)
    {
        
        GlobalItem temp = globalItemList._GlobalItemList.Find(o => o.ID == itemID);
        if (temp != null)
        {                  
            temp.Quantity -= itemQuantity;
        }
    }
}
