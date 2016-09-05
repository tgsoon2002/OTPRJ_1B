using System;
using UnityEngine;

namespace ObjectTypes
{
	public enum CharacterClassType
	{
		MELEE,
		RANGED
	};

	public enum SystemDataType
	{
		COMBATSYSYEM,
		SQUADMANAGER,
		CAMERAMANAGER,
		ITEMUSEMANAGER,
        GAMEMANAGER
	};

    public enum GameStateType
    {
        COMBATMODE,
        EXPEDITIONMODE
    };

    public enum BaseItemType
    {
        USEABLE,
        NONUSEABLE,
    };

    public enum ItemRarity
    {
        COMMON,
        UNCOMMON,
        RARE,
        VERYRARE
    };
}

