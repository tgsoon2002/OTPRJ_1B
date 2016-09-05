using UnityEngine;
using System.Collections;
using ObjectTypes;

public class BaseItem : MonoBehaviour 
{
    protected string name;
    protected string description;
    protected BaseItemType baseItemType;
    protected int itemPrice;
    public Sprite itemIcon;
    protected int itemID;
    protected ItemRarity itemRarity;

    protected string _Name
    {
        get { return name; }
        set { name = value; }
    }

    protected string _Description
    {
        get { return description; }
        set { description = value; }
    }

    protected BaseItemType _BaseItemType
    {
        get { return baseItemType; }
        set { baseItemType = value; }
    }

    protected int _ItemPrice
    {
        get { return itemPrice; }
        set { itemPrice = value; }
    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
