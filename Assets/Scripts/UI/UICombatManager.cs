using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICombatManager : MonoBehaviour
{

	GameObject activeBtn;
	GameObject itemPanel;
	GameObject skillPanel;
	GameObject pauseBtn;
	public Sprite itemBtnTexture;
	public Sprite skillBtnTexture;
	public bool itemMode = true;

	static UICombatManager _instance;

	public static  UICombatManager Instance {
		get{ return  _instance; }
		set{ _instance = value; }
	}

	/// <summary>
	/// Switchs the button. between consumableItem and active skill
	/// </summary>
	/// <param name="mode">If set to <c>true</c> mode.</param>
	public bool ItemMode {
		get{ return  itemMode; }
		set {
			itemMode = value;
			if (value)
				activeBtn.GetComponent<Image> ().sprite = itemBtnTexture;
			else
				activeBtn.GetComponent<Image> ().sprite = skillBtnTexture;
		}
	}

	void Awake ()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		activeBtn = transform.GetChild (0).gameObject;
		itemPanel = transform.GetChild (1).gameObject;
		skillPanel = transform.GetChild (2).gameObject;
		pauseBtn = transform.GetChild (3).gameObject;
		_ResumeBattle ();
	}


	public void DisableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", false);

	}

	public void EnableBoolAnimator (Animator anim)
	{
		anim.SetBool ("IsDisplayed", true);
		GUI.color = Color.black;
	}

	public void NavigateTo (int scene)
	{
		Application.LoadLevel (scene);
	}



	public void _ResumeBattle ()
	{
		activeBtn.SetActive (true);
		itemPanel.SetActive (false);
		skillPanel.SetActive (false);
		pauseBtn.SetActive (false);
	}

	public void _OpenAction ()
	{
		activeBtn.SetActive (false);
		if (itemMode) {
			itemPanel.SetActive (true);
			skillPanel.SetActive (false);	
		} else {
			itemPanel.SetActive (false);
			skillPanel.SetActive (true);
		}
		pauseBtn.SetActive (true);
	}



	public void _OpenSetting ()
	{

	}

	public GameObject StartCutScene ()
	{
		transform.GetChild (4).gameObject.SetActive (true);
		return transform.GetChild (4).gameObject;
	}

	public void EndCutScene ()
	{
		transform.GetChild (4).gameObject.SetActive (false);
	}

}
