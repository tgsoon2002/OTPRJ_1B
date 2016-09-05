using UnityEngine;
using System.Collections;

public interface ICharacterStats 
{	
	//May add or remove methods to this
	//interface later. 

	float Max_Character_Heralth
	{
		get;
		set;
	}

	float Current_Chacter_Health
	{
		get;
		set;
	}

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

	float Move_Speed
	{
		get;
		set;
	}

	float Attack_Damage
	{
		get;
		set;
	}

	float Attack_Range
	{
		get;
		set;
	}

	float Physical_Defense
	{
		get;
		set;
	}
}
