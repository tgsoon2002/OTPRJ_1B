using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CharacterCombat : MonoBehaviour
{
	Collider2D col;
	public float healthPoints;
	public float damageValue;

	public Material nonSelectMat;
	public Material selectedMat;

	Touch finger;
	bool pickedEnemy = false;
	bool isTappedBegin = false;
	bool isTapped = false;
	bool isTappedEnd = false;
	List<GameObject> meleeObjects;
	private static CharacterCombat instance;
	public Character currentChar;

	public static CharacterCombat _instance {
		get { return instance; }
	}

	void Awake ()
	{
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		/*
        meleeObjects = new List<GameObject> (FindObjectsOfType(typeof (Character)) as GameObject[]);
        Debug.Log("Number of characters: " + meleeObjects.Count);
        */
		GameObject[] tmp = GameObject.FindGameObjectsWithTag ("Player");
		meleeObjects = new List<GameObject> ();

		foreach (GameObject e in tmp) {
			if (e.GetComponent<Character> ().testClassType == "Melee") {
				meleeObjects.Add (e);
			}
		}
       

	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (Input.touchCount > 0) {
			Vector2 deltaPosition = Input.GetTouch (0).deltaPosition;

			switch (Input.GetTouch (0).phase) {
			case TouchPhase.Began:
				if (Input.touchCount == 1) {
					Vector3 wp = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					Vector2 touchPosition = new Vector2 (wp.x, wp.y);

					RaycastHit hit;

					col = Physics2D.OverlapPoint (touchPosition);

					if (col != null && col.tag == "Enemy") {
						isTappedBegin = true;
						Debug.Log (col.gameObject.name.ToString ());
					}     
				}
				break;
               
			case TouchPhase.Ended:

				if (isTappedBegin == true) {
					Debug.Log ("Tapped that bitch!!!");
					TestCombat (col.gameObject);
					isTappedEnd = true;
					isTappedBegin = false;
				}
				break;
			}
		}
	}

	public void DestroyGameObject (GameObject character)
	{
		if (meleeObjects.Remove (character)) {
			Destroy (character);
		}           
	}

	private void TestCombat (GameObject enemy)
	{
		float distance;
		float obstacleDistance = 0.0f;
		GameObject temp = null;

		foreach (GameObject o in meleeObjects) {
			distance = Vector2.Distance (enemy.transform.position, o.transform.position);

			if (distance <= o.GetComponent<Character> ().charAttackRange) {
				RaycastHit2D[] hit = Physics2D.RaycastAll (o.transform.position, enemy.transform.position);

				foreach (RaycastHit2D elt in hit) {
					if (elt.collider.tag == "Obstacle") {
						temp = elt.collider.gameObject;
					}
				}

				if (temp != null) {
					obstacleDistance = Vector2.Distance (temp.transform.position, o.transform.position);
				}

				if (obstacleDistance > distance || obstacleDistance == 0.0f) {
					Debug.Log (o.GetComponent<Character> ().name + " does a melee attack!!!!");
				} else {
					Debug.Log (o.GetComponent<Character> ().name + " fails a melee attack!!!!");
				}
			}
		}
	}
}
