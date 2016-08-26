using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICombatManager : MonoBehaviour
{

	GameObject activeBtn;
	GameObject itemPanel;
	public GameObject SkillPatten;
	GameObject pauseBtn;
	GameObject cutScene;
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
		activeBtn = transform.GetChild (0).gameObject;
		itemPanel = transform.GetChild (1).gameObject;
		cutScene = transform.GetChild (2).gameObject;
		pauseBtn = transform.GetChild (3).gameObject;
	}

	// Use this for initialization
	void Start ()
	{
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


	/// <summary>
	/// Resumes the battle.
	/// </summary>
	public void _ResumeBattle ()
	{
		activeBtn.SetActive (true);
		itemPanel.SetActive (false);
		SkillPatten.GetComponent<SkillManager> ().IsSkillScreenOpen = false;
		pauseBtn.SetActive (false);
		Time.timeScale = 1f;
	}

	/// <summary>
	/// Opens the action. When action button was click 
	/// </summary>
	public void _OpenAction ()
	{
		Time.timeScale = 0.1f;
		activeBtn.SetActive (false);
		pauseBtn.SetActive (true);
		if (itemMode) {
			itemPanel.SetActive (true);
			SkillPatten.GetComponent<SkillManager> ().IsSkillScreenOpen = false;
		} else {
			itemPanel.SetActive (false);
			SkillPatten.GetComponent<SkillManager> ().IsSkillScreenOpen = true;
			//SkillPatten.GetComponent<SkillManager> ().ChangeSKillSet (currentChar.skillSet, currentChar.currentMana, currentChar.maxMana);
		}

	}

	/// <summary>
	/// Opens the setting menu.
	/// </summary>
	public void _OpenSetting ()
	{
		Time.timeScale = 0f;
	}

	/// <summary>
	/// Starts the cut scene.
	/// </summary>
	/// <returns>The cut scene.</returns>
	public GameObject StartCutScene ()
	{
		cutScene.SetActive (true);
		activeBtn.SetActive (false);
		itemPanel.SetActive (false);
		pauseBtn.SetActive (false);
		return cutScene;
	}

	/// <summary>
	/// Ends the cut scene.
	/// </summary>
	public void EndCutScene ()
	{
		cutScene.SetActive (false);
	}

}
