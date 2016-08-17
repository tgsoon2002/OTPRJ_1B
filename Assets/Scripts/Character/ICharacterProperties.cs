using UnityEngine;
using System.Collections;

public interface ICharacterProperties
{
	bool Is_Selected
	{
		get;
	}

	bool Is_Moving
	{
		get;
		set;
	}

	void SelectCharacter();
}
