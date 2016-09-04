using UnityEngine;
using System.Collections;

public interface IConsumeableItem
{
    string Item_Name
    {
        get;
    }

    int Item_Quantity
    {
        get;
        set;
    }

    Sprite Item_Icon
    {
        get;
    }

    string Item_Description
    {
        get;
    }
}

