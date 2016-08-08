using UnityEngine;
using System.Collections;

public class StaminaManager : MonoBehaviour 
{
	#region Data Members

	private ICharacterStats charStats;
	private bool isStaminaOnRegen = false;

	[SerializeField]
	private float currentStamina;
	[SerializeField]
	private float staminaRegenRate = 3.0f;

	#endregion

	#region Setters & Getters

	// Use this for initialization
	void Start ()
	{
		charStats = gameObject.GetComponent<ICharacterStats>();
		currentStamina = charStats.Current_Character_Stamina;
	}

	// Update is called once per frame
	void Update () 
	{
		if(charStats.Current_Character_Stamina < charStats.Max_Character_Stamina 
			&& !isStaminaOnRegen)
		{
			StartCoroutine(StaminaRegen());
		}
	}

	#endregion

	#region Built-in Unity Methods

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private IEnumerator StaminaRegen()
	{
		isStaminaOnRegen = true;

		while(charStats.Current_Character_Stamina < charStats.Max_Character_Stamina)
		{
			if(charStats.Current_Character_Stamina + staminaRegenRate > charStats.Max_Character_Stamina)
			{
				charStats.Current_Character_Stamina += staminaRegenRate;
			}
			else
			{
				charStats.Current_Character_Stamina += (charStats.Max_Character_Stamina 
														- (currentStamina + staminaRegenRate));
			}

			if(charStats.Current_Character_Stamina >= charStats.Max_Character_Stamina)
			{
				isStaminaOnRegen = false;
			}

			yield return new WaitForSeconds(1.0f);
		}
	}

	#endregion
}
