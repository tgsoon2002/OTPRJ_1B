using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
	#region Data Members

	private static CombatSystem _instance;
	private FloatingDamageText dmgText;
	private GameObject canvasRef;

	#endregion

	#region Setters & Getters

	public static CombatSystem Instance
	{
		get 
		{ 
			if(!_instance)
			{
				try
				{
					_instance = FindObjectOfType(typeof(CombatSystem)) as CombatSystem;

				}
				catch 
				{
					Debug.LogError("No CombatSystem GameObject detected in scene!");
				}
			}

			return _instance;
		}
	}

	#endregion

	#region Built-in Unity Methods

	#endregion

	#region Public Methods

	/// <summary>
	/// Deals the physical damage.
	/// </summary>
	/// <param name="victim">Victim.</param>
	/// <param name="value">Value.</param>
	public void DealPhysicalDamage(GameObject victim, float value)
	{
		//Declaring local variables
		ICharacterStats victimStats = gameObject.GetComponent<ICharacterStats>();
		ICharacterProperties victimProperties = gameObject.GetComponent<ICharacterProperties>();

		//HACK -- Mark for: Subject To Change
		//We might change how damage values are calculated
		victimStats.Current_Chacter_Health -= (value - victimStats.Physical_Defense);

		PlayDamageParticleEffect(victim, value - victimStats.Physical_Defense);

		if(victimStats.Current_Chacter_Health <= 0.0f)
		{
			victimProperties.DestroyCharacter();
		}
	}

	/// <summary>
	/// Deals the magic damage.
	/// </summary>
	/// <param name="victim">Victim.</param>
	/// <param name="value">Value.</param>
	public void DealMagicDamage(GameObject victim, float value)
	{
		//TBD
	}

	#endregion

	#region Private Methods

	/// <summary>
	/// Initialize this instance.
	/// </summary>
	void Init()
	{
		canvasRef = GameObject.Find("Canvas");
		dmgText = Resources.Load<FloatingDamageText>("Prefabs/PopupTextParent");
	}
		
	/// <summary>
	/// Call this function to display the damage
	/// values as a Particle Effect, Slash effects, etc.
	/// </summary>
	void PlayDamageParticleEffect(GameObject victim, float val)
	{
		//Declaring local variables
		FloatingDamageText txtInstance = Instantiate(dmgText);
		Vector2 screenPos = Camera.main.WorldToScreenPoint(victim.transform.position);

		txtInstance.transform.SetParent(canvasRef.transform, false);
		txtInstance.transform.position = screenPos;
		txtInstance.SetDamageValueText(val);
	}

	#endregion
}
