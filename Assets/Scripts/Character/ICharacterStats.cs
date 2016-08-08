using UnityEngine;
using System.Collections;

public interface ICharacterStats 
{	
	//May add or remove methods to this
	//interface later. 
	float Max_Character_Stamina
	{
		get;
		set;
	}

	float Current_Character_Stamina
	{	
		get;
		set;
	}

	float Attack_Speed
	{
		get;
		set;
	}
}
