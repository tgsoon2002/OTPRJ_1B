using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenScaler : MonoBehaviour 
{
    CanvasScaler screenScaler;
    float screenDpi;

	// Use this for initialization
	void Start () 
    {
        screenScaler = gameObject.GetComponent<CanvasScaler>();

        screenDpi = Screen.dpi;

        if (screenDpi > -1)
        {
            screenScaler.defaultSpriteDPI = screenDpi;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
