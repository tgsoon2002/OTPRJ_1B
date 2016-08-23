using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPlayerDeployedConsumableItemInfo  
{
    List<IConsumeableItem> Deployed_Consumable_Items
    {
        get;
        set;
    }

}
