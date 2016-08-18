using UnityEngine;
using System.Collections;

public class TestTouchHandler : MonoBehaviour
{
	Ray screenRay;
	RaycastHit hit;
	Plane hitPlane;
	float hitFloat;
	Collider col;
	public string derp;

	// Use this for initialization
	void Start () 
	{
		hitPlane = new Plane(gameObject.transform.forward, gameObject.transform.up);
		col = gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetMouseButton(0))
//		{
//			screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//
//			if(Physics.Raycast(screenRay, out hit))
//			{
//				if(hit.collider.tag == "Player")
//				{
//					Debug.Log("Ouch, my name is: " + derp);
//				}
//			}
////			if(hitPlane.Raycast(screenRay, out hitFloat))
////			{
////				
////			}
//
////			if(col.Raycast(screenRay, out hit, Mathf.Infinity))
////			{
////				Debug.Log("Ouch, my name is: " + derp);
////			}
//		}
	}
}
