using UnityEngine;
using System.Collections;

public class TestCaller : MonoBehaviour {

	public TestNullRef dude;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(dude != null)
			Debug.Log("Obj's ok");
		else
			Debug.Log("uh oh");

		if(Input.GetKey(KeyCode.A))
		{
			Destroy(dude.gameObject);
		}
	}
}
