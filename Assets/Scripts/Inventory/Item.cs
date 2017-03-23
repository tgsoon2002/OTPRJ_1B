using UnityEngine;
using System.Collections;

public class Item
{
    public int _ItemID;
    public string _ItemTitle;
    public int _ItemValue;
    public int _Power;
    public int _Defence;
    public int _Vitality;
    public string _Description;
    public bool _IsStackable;
    public int _Rarity;

    public string _ItemSprite;


    public int ID
    {
        get { return _ItemID; }
        set { _ItemID = value; }
    }

    public string ItemName
    {
        get { return _ItemTitle; }
        set { _ItemTitle = value; }
    }

    public int ItemValue
    {
        get { return _ItemValue; }
        set { _ItemValue = value; }
    }

    public int Power
    {
        get { return _Power; }
        set { _Power = value; }
    }

    public int Defence
    {
        get { return _Defence; }
        set { _Defence = value; }
    }

    public int Vitality
    {
        get { return _Vitality; }
        set { _Vitality = value; }
    }

    public string Description
    {
        get { return _Description; }
        set { _Description = value; }
    }

    public bool IsStackable
    {
        get { return _IsStackable; }
        set { _IsStackable = value; }
    }

    public int Rarity
    {
        get { return _Rarity; }
        set { _Rarity = value; }
    }


    public string ItemSprite

    {
        get { return _ItemSprite; }
        set { _ItemSprite = value; }
    }


    public Item(int itemID, string itemName, int itemValue, string itemSprite,
                 int power, int defence, int vitality, string description,
                 bool isStackable, int rarity)
    {
        this._ItemID = itemID;
        this._ItemTitle = itemName;
        this._ItemValue = itemValue;

        this._ItemSprite = itemSprite;

        this._Power = power;
        this._Defence = defence;
        this._Vitality = vitality;
        this._Description = description;
        this._IsStackable = isStackable;
        this._Rarity = rarity;
    }

    public Item()
    {
        this._ItemID = -1;
    }
}
