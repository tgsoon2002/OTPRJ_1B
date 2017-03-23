using UnityEngine;
using System.Collections;

public class GlobalItem 
{
    public int _ItemID;  
    public int _ItemQuantity;

    public int ID
    {
        get { return _ItemID; }
        set { _ItemID = value; }
    }

    public int Quantity
    {
        get { return _ItemQuantity; }
        set { _ItemQuantity = value; }
    }

    public GlobalItem (int itemID, int quantity)
    {
        this._ItemID = itemID;
        this._ItemQuantity = quantity;
    }

    public GlobalItem ()
    {
        this._ItemID = -1;
    }
}
