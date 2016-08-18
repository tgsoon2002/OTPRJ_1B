using UnityEngine;
using System.Collections;

public class TTchManager : MonoBehaviour 
{
	Ray screenRay;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0))
		{
			screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(screenRay, out hit))
			{
				if(hit.collider.tag == "Player")
				{
					Debug.Log("Ouch, my name is: " + hit.collider.gameObject.GetComponent<TestTouchHandler>().derp);
				}
			}
			//			if(hitPlane.Raycast(screenRay, out hitFloat))
			//			{
			//				
			//			}

			//			if(col.Raycast(screenRay, out hit, Mathf.Infinity))
			//			{
			//				Debug.Log("Ouch, my name is: " + derp);
			//			}
		}
	
	}
}
