using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisableBoolAnimator (Animator anim)
    {
        anim.SetBool("IsDisplayed", false);

    }

    public void EnableBoolAnimator (Animator anim)
    {
        anim.SetBool("IsDisplayed", true);
        GUI.color = Color.black;
    }

    public void NavigateTo (int scene)
    {
        Application.LoadLevel(scene);
    }


}
