using UnityEngine;
using System.Collections;
using ObjectTypes;

public class MeleeBasicAttack : MonoBehaviour 
{
	#region Data Members

	private Touch touchInput;
	private TouchPhase touchPhase;
	private Ray screenRay;
	private RaycastHit hit;

	//HACK -- Marked for change:
	//Will put this value somewhere else for later!
	[SerializeField]
	private float testAttackStaminaCost = 15.0f;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount > 0)
		{
			touchInput = Input.GetTouch(0);
			touchPhase = touchInput.phase;

			if(touchPhase == TouchPhase.Began)
			{
				screenRay = Camera.main.ScreenPointToRay(touchInput.position);

				if(Physics.Raycast(screenRay, out hit))
				{
					if(hit.collider.tag == "Enemy")
					{
						//Check for valid range and valid stamina.
						if(EvaluateMeleeRange(hit.collider.gameObject) && gameObject.GetComponent<ICharacterStats>().Current_Character_Stamina > testAttackStaminaCost)
						{
							//Call the combat system to do its thing.
							CombatSystem.Instance.DealPhysicalDamage(hit.collider.gameObject, gameObject.GetComponent<ICharacterStats>().Attack_Damage);
							gameObject.GetComponent<ICharacterStats>().Current_Character_Stamina -= testAttackStaminaCost;
						}
					}
				}
			}
		}
	}
	
	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	/// <summary>
	/// Evaluates the melee range.
	/// </summary>
	/// <returns><c>true</c>, if melee range was evaluated, <c>false</c> otherwise.</returns>
	/// <param name="enemy">Enemy.</param>
	private bool EvaluateMeleeRange(GameObject enemy)
	{
		//Declaring local variables
		float atkRange = Vector3.Distance(gameObject.transform.position, enemy.transform.position);
		bool isInRange = false;

		if(atkRange <= gameObject.GetComponent<ICharacterStats>().Attack_Range)
		{
			isInRange = true;
		}

		return isInRange;
	}
		
	#endregion
}
