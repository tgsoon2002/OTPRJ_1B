using UnityEngine;
using System.Collections;

public class TestJaysBalls : MonoBehaviour 
{
    public bool touched = false;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                if (touched == true)
                {
                    Destroy(gameObject);
                }
            }
        }
	}
}
