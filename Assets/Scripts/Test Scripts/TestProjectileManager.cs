using UnityEngine;
using System.Collections;

public class TestProjectileManager : MonoBehaviour 
{
	Vector3 direction = Vector3.up;
	float speed = 5.0f;
	float timer = 5.0f;
	public GameObject projectilePrefab;

	// Use this for initialization
	void Start () 
	{
		GameObject tmp = Instantiate(projectilePrefab);

		Debug.Log("wtf");

		tmp.transform.position = new Vector3(0.0f, -5.0f, 0.0f);
		tmp.GetComponent<OffensiveRangedProperty>().AddForceWithGivenDirectionOnProjectile(direction, speed, 3.0f, timer);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
