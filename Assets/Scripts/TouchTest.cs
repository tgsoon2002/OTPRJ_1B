using UnityEngine;
using System.Collections;

public class TouchTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Used to test input
        //Debug.Log(Input.touchCount);

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Shhh. Only dreams.....");
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log ("Hold her tighter, she's a fighter!!!"); 
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Debug.Log ("Innocence lost."); 
            }
        }

	}
}
