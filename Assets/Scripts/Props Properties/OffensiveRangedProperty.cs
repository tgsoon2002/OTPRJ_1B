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

	[SerializeField]
	private Vector3 direction;
	private Vector3 initialPosition;
	private float stayAliveDistance = 0.0f;
	private float stayAliveTimer = 0.0f;
	private float damageValue;
	private float speedValue;
	private bool timerTriggered = false;
	private bool distanceTriggered = false;

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

	//Using Awake() for initialization.
	void Awake()
	{
		physics = GetComponent<Rigidbody>();
		objCollider = GetComponent<Collider>();
		//physics.freezeRotation = true;
	}

	void Update()
	{
		if(timerTriggered && stayAliveTimer <= 0)
		{
			Destroy(gameObject);
		}

		if(distanceTriggered && Vector3.Distance(initialPosition, gameObject.transform.position) >= stayAliveDistance)
		{
			Destroy(gameObject);
		}
	}
		
	//When the projectile hits a collider that is
	//non-Trigger type, destroy it.
	void OnCollisionEnter(Collision col)
	{
		Debug.Log("Collided with: " + col.gameObject.name);
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
	public void AddForceWithGivenDirectionOnProjectile(Vector3 dir, float spd, float stayAliveDist, float _stayAliveTimer)
	{
		Vector3 normDir = Vector3.Normalize(dir);

		stayAliveTimer = _stayAliveTimer;
		stayAliveDistance = stayAliveDist;
		initialPosition = gameObject.transform.position;

		if(stayAliveDist > 0.0f)
		{
			distanceTriggered = true;
		}

		if(stayAliveTimer > 0.0f)
		{
			timerTriggered = true;
		}

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
