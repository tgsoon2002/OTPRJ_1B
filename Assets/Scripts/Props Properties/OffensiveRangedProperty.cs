using UnityEngine;
using System.Collections;

/// <summary>
/// Use this class for projectile objects with a RigidBody
/// component on the screen, i.e. Arrows, Fireballs, etc.
/// </summary>
public class OffensiveRangedProperty : MonoBehaviour
{
	#region Data Members

	private Collider objCollider;
	private Rigidbody physics;
	private Vector3 direction;
	private float stayAliveDistance = 0.0f;
	private float stayAliveTimer = 0.0f;
	private float damageValue;
	private float speedValue; 

	#endregion

	#region Setters & Getters

	/// <summary>
	/// Gets the particle damage value.
	/// </summary>
	/// <value>The particle damage value.</value>
	public float Particle_Damage_Value
	{
		get { return damageValue; }
	}

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start ()
	{
		physics = GetComponent<Rigidbody>();
		objCollider = GetComponent<Collider>();
	}

	//When the projectile hits a collider that is
	//non-Trigger type, destroy it.
	void OnCollision(Collider col)
	{
		Destroy(gameObject);
	}
		
	#endregion

	#region Public Methods

	#region Method: AddForceWithGivenDirectionOnProjectile & Overloads 

	/// <summary>
	/// Initializes the projectile to go to a straight line. 
	/// </summary>
	/// <param name="dir">Dir.</param>
	/// <param name="spd">Spd.</param>
	public void AddForceWithGivenDirectionOnProjectile(Vector3 dir, float spd)
	{
		//Assign static values to distance and timer.
		AddForceWithGivenDirectionOnProjectile(dir, spd, 0.0f, 5.0f);
	}

	/// <summary>
	/// Initializes the projectile to go to a straight line.
	/// </summary>
	/// <param name="dir">Dir.</param>
	/// <param name="spd">Spd.</param>
	/// <param name="stayAliveDist">Stay alive dist.</param>
	public void AddForceWithGivenDirectionOnProjectile(Vector3 dir, float spd, float stayAliveDist)
	{
		//Assign some static value for the timer.
		AddForceWithGivenDirectionOnProjectile(dir, spd, stayAliveDist, 5.0f);
	}

	/// <summary>
	/// Initializes the projectile to go to a straight line.
	/// </summary>
	/// <param name="dir">Dir.</param>
	/// <param name="spd">Spd.</param>
	/// <param name="stayAliveDist">Stay alive dist.</param>
	/// <param name="stayAliveTimer">Stay alive timer.</param>
	public void AddForceWithGivenDirectionOnProjectile(Vector3 dir, float spd, float stayAliveDist, float stayAliveTimer)
	{
		StartCoroutine(CountdownTimer());
		StartCoroutine(AddForceAndDestroyOnSpecifiedParam(dir, spd, stayAliveTimer));
	}
		
	#endregion

	#endregion

	#region Private Methods

	/// <summary>
	/// This coroutine shall continually add force
	/// to the projectile so long as the timer does
	/// not reach zero.
	/// </summary>
	/// <returns>The force and destroy on specified parameter.</returns>
	/// <param name="dir">Dir.</param>
	/// <param name="spd">Spd.</param>
	/// <param name="stayAliveTimer">Stay alive timer.</param>
	IEnumerator AddForceAndDestroyOnSpecifiedParam(Vector3 dir, float spd, float stayAliveTimer)
	{
		while(stayAliveTimer > 0)
		{
			physics.AddForce(dir * spd);
			yield return null;
		}
	}

	/// <summary>
	/// Coroutine used to act as a timer
	/// before projectile is destroyed.
	/// </summary>
	/// <returns>The timer.</returns>
	IEnumerator CountdownTimer()
	{
		while(stayAliveTimer > 0)
		{
			stayAliveTimer--;
			yield return new WaitForSeconds(1.0f);
		}
	}

	#endregion
}
