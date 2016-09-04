using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public GameObject item;
	public GameObject itemContainer;
	public ItemDatabase itemDatabase;
	public UIManager uiManager;
	public List<Item> itemList = new List<Item> ();
	public List<GameObject> inventoryItems = new List<GameObject> ();

	void Start ()
	{
		AddItem (0);
		AddItem (1);
	}

	public void AddItem (int itemID)
	{
		Item itemToAdd = itemDatabase.FetchItemByID (itemID);
		GameObject itemObj = Instantiate (item);
		itemObj.transform.SetParent (itemContainer.transform);
		itemObj.transform.position = Vector2.zero;
		itemObj.transform.localScale = Vector2.one;
		itemObj.GetComponent<testItem> ().uiManager = uiManager;
		itemObj.GetComponent<testItem> ().ChangeInfo (itemToAdd, 2);
	}
}
