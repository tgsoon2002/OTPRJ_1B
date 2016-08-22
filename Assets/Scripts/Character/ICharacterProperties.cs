using UnityEngine;
using System.Collections;
using ObjectTypes;

public interface ICharacterProperties
{
	bool Is_Selected
	{
		get;
		set;
	}

	bool Is_Moving
	{
		get;
		set;
	}

	CharacterClassType Character_Class_Type
	{
		get;
	}

	void SelectCharacter();
	void DestroyCharacter();
}
