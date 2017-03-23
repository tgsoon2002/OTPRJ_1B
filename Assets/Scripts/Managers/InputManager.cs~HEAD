using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    GameObject playerReference;
    RaycastHit hit;
    Ray ray;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount > 0)
        {   
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:

                    if (Input.touchCount == 1)
                    {
                        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.tag == "Player")
                            {
                                playerReference = hit.collider.gameObject;
                                playerReference.GetComponent<TestJaysBalls>().touched = true;
                            }
                        }
                    }
                   
                    break;

                case TouchPhase.Moved:
                    if (playerReference != null)
                    {
                        
                    
                        if (playerReference.GetComponent<TestJaysBalls>().touched == true)
                        {
                            Debug.Log("Touched by Dan Schneider....");
                        }
                    }
                    break;

                case TouchPhase.Ended:

                    break;
            }
        }
    }
}
