using UnityEngine;
using System.Collections;

public class RangedCharacterAI : MonoBehaviour 
{
	#region Data Members

	public GameObject projectile;
	private Collider sightCollider;
	private bool isOnCoolDown = false;
	private Transform shooter;

	[SerializeField]
	private float basicRangedAttack = 20.0f;

	[SerializeField]
	private float tempProjectileSpeed = 1.0f;	//Generalize this later!

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start() 
	{
		sightCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider>();
		shooter = gameObject.transform.GetChild(1);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Enemy")
		{
			ICharacterStats pChar = gameObject.transform.GetComponentInParent<ICharacterStats>();

			if(pChar.Current_Character_Stamina >= basicRangedAttack && !isOnCoolDown)
			{
				GameObject tmp = Instantiate(projectile);
				tmp.transform.SetParent(shooter.transform);
				tmp.transform.position = shooter.transform.position;
				tmp.GetComponent<OffensiveRangedProperty>().
					AddForceWithGivenDirectionOnProjectile(col.gameObject.transform.position, tempProjectileSpeed);
				gameObject.GetComponent<ICharacterStats>().Current_Character_Stamina -= basicRangedAttack;
				StartCoroutine(AttackCoolDown(gameObject.GetComponent<ICharacterStats>().Attack_Speed));
			}
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private IEnumerator AttackCoolDown(float timer)
	{
		isOnCoolDown = true;
		yield return new WaitForSeconds(timer);
		isOnCoolDown = false;
	}

	#endregion
}
