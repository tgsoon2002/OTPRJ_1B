using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	string enemyLayer = "Enemy";
	public float AttackDamage;
	// Use this for initialization
	public string EnemyLayer {
		get{ return  enemyLayer; }
		set{ enemyLayer = value; }
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter (Collider target)
	{
		Debug.Log ("HItting" + enemyLayer + " " + AttackDamage + " damage");
		if (target.CompareTag (enemyLayer)) {
			target.GetComponent<BaseCharacter> ().TakeDamage (AttackDamage, true);
			AttackDamage = 0.0f;
		}
	}
}
