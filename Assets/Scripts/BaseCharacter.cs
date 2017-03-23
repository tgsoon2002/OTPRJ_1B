using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour
{

	#region Member



	protected UnitStats statTable;
	[SerializeField]
	protected string characterName;
	protected float sightRange;
	[SerializeField]
	public HealthBar HB;
	public HealthBar SB;

	protected bool isDead = false;

	#endregion


	#region Prop

	public float PercentHealth {
		get{ return  statTable.AttributeValue (12) / statTable.AttributeValue (0); }
	}

	public float PercentMana {
		get{ return  statTable.AttributeValue (13) / statTable.AttributeValue (1); }
	}

	public float PercentStamina {
		get{ return  statTable.AttributeValue (14) / statTable.AttributeValue (2); }
	}

	public virtual UnitStats CharacterStats {
		get{ return statTable; }
		set{ statTable = value; }
	}

	#endregion



	#region UnitBuiltInMethod

	void Update ()
	{
		// constant regen Mana
		if (statTable.AttributeValue (13) < statTable.AttributeValue (1)) {
			statTable.AttributeChange (statTable.AttributeValue (9) * Time.deltaTime, 13);
		}

		// constant regen Stamina
		if (statTable.AttributeValue (14) < statTable.AttributeValue (2)) {
			statTable.AttributeChange (statTable.AttributeValue (8) * Time.deltaTime, 14);
			SB.BarValue = statTable.AttributeValue (14) / statTable.AttributeValue (2);
		}

	}

	#endregion



	#region MainMethod

	//	/// <summary>
	//	/// Moves the this unit. by add velocity to character.
	//	/// </summary>
	//	/// <param name="direction">Direction.</param>
	//	public virtual void MoveThisUnit (float direction)
	//	{
	//		rig.velocity = new Vector3 (direction * statTable.AttributeValue (6), rig.velocity.y);
	//		anim.SetFloat ("speed", direction * statTable.AttributeValue (6));
	//	}



	/// <summary>
	/// Determines whether this character is dead. if health is less or equal 0
	/// if dead, set isDead = true, play dead animation and destroy GO after 2 sec(this may change later )
	/// </summary>
	/// <returns><c>true</c> if this instance is dead; otherwise, <c>false</c>.</returns>
	public virtual bool IsDead ()
	{
		if (statTable.AttributeValue (12) <= 0) {
			Destroy (this, 2.0f);
			isDead = true;
		}
		return  isDead;
	}

	/// <summary>
	/// Character takes the damage, 
	/// </summary>
	/// <param name="damageValue">Damage value.</param>
	/// <param name="isPhysicalDamage">If set to <c>true</c> is physical damage.</param>
	public	virtual void TakeDamage (float damageValue, bool isPhysicalDamage)
	{
		// If character is dead, then not take anymore damage, 
		// Before take damage, reduce damage base on type.

		if (!isDead) {
			float tempDamage = ReduceByDefence (damageValue, isPhysicalDamage);
			statTable.AttributeChange (tempDamage * -1, 12);
			IsDead ();
			HB.BarValue = PercentHealth;	
		}

	}

	public virtual void Kill ()
	{
		statTable.AttributeChange (0.0f, 12);
	}

	#endregion

	#region Helper Method

	float ReduceByDefence (float damage, bool isPhysicalDamage)
	{
		float tempDamage = damage;
		if (isPhysicalDamage) {
			tempDamage -= statTable.AttributeValue (10);
		} else {
			tempDamage -= statTable.AttributeValue (11);
		}
		if (tempDamage > 0) {
			return tempDamage;
		} else {
			return 0;
		}
	}

	#endregion

}