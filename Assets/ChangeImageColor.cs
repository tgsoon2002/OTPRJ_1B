using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeImageColor : MonoBehaviour 
{
    //    Image image;

	// Use this for initialization
	void Start () 
    {
        //image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void NewGame ()
    {
        Debug.Log("New Game!!!");
        SceneManager.LoadScene("New_Game_Menu_Scene");
    }

    public void LoadGame ()
    {
        Debug.Log("Load Game!!!");
    }

    public void Options ()
    {
        Debug.Log("Options!!!");
    }

    public void Exit ()
    {
        Debug.Log("Exit!!!");
        Application.Quit();

    }
}
