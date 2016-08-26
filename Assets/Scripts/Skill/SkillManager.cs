using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillManager : MonoBehaviour
{
	#region Data Members

	public Camera cam;
	Ray ray;
	RaycastHit hit;
	bool drawing = false;
	float currentMana = 100;
	float maxMana = 100;


	public Material before;
	public Material after;
	bool isSkillScreenOpen = false;
	public int skillActiveID;
	Transform skillContainer;
	SkillSet[] skillSet;
	SkillNode endNode;
	SkillNode nextNode;
	bool skillSuccess = false;
	SkillSet currentSet;

	public GameObject canvas;
	public GameObject pattern;
	public Image[] backgroundImageButtonList;
	Touch touchPos;


	#endregion

	#region Setters & Getters

	public bool IsSkillScreenOpen {
		get{ return  isSkillScreenOpen; }
		set {
			isSkillScreenOpen = value;
			canvas.SetActive (value);
			pattern.SetActive (value);

		}
	}

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start ()
	{

		if (skillSet [skillActiveID] != null) {
			_ChangeSkill (skillActiveID);
		}

	}

	// Update is called once per frame
	void Update ()
	{
		if (isSkillScreenOpen && Input.touchCount > 0) {
			touchPos = Input.GetTouch (0);
			// when first put the finger down.
			if (touchPos.phase == TouchPhase.Began) {
				GetComponent<TrailRenderer> ().Clear ();
				Debug.Log ("touch start");

			}
			// whem move the finger around.
			if (touchPos.phase == TouchPhase.Moved) {

				// reduce stamina when drawing the patten.
				ReduceMana ();
				// move the oject follow the mouse position to do the logic.
				transform.position = cam.ScreenToWorldPoint (new Vector3 (touchPos.position.x, touchPos.position.y, 2f));
				ray = cam.ScreenPointToRay (new Vector3 (touchPos.position.x, touchPos.position.y, 5f));
				if (Physics.Raycast (ray, out hit, 20f)) {
					if (hit.collider.tag == "skillNode") {
						// if hit the node in the middle, then set the next nodel
						if (hit.collider.GetComponent<SkillNode> () == nextNode && nextNode != endNode && hit.collider.gameObject.activeSelf == true) {
							hit.collider.GetComponent<MeshRenderer> ().material = after;
							nextNode = hit.collider.GetComponent<SkillNode> ().nexNode;
						} 
						// if hit the last node. then skill success and call the active skill success to active the skill effect.		
						else if (hit.collider.GetComponent<SkillNode> () == nextNode && nextNode == endNode) {
							ActiveSkillSuccess ();
							hit.collider.GetComponent<MeshRenderer> ().material = after;
						}
					}
				}
			}
			// if lift the finger. when skill not done then It will cancel the skill.
			if (Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetTouch (0).phase == TouchPhase.Canceled) {
				if (!skillSuccess) {
					ActiveSkillFail ();

				}
			}
		}
	}

	#endregion

	#region Public Methods

	/// <summary>
	/// Changes the skill.
	/// This will change the current patten to the proper skill being choosed.
	/// </summary>
	/// <param name="skillID">Skill I.</param>
	public void _ChangeSkill (int skillID)
	{
		// remove the old pattern if there exist.
		if (currentSet != null) {
			Destroy (currentSet.gameObject);
		}
		// create the pattern
		GameObject temp = Instantiate (skillSet [skillID].gameObject);
		temp.transform.SetParent (skillContainer);
		temp.transform.localPosition = Vector3.zero;
		temp.SetActive (true);
		currentSet = temp.GetComponent<SkillSet> ();
		// setup the active skill background.
		canvas.transform.GetChild (0).GetChild (2).GetComponent<Image> ().sprite = temp.GetComponent<SkillSet> ().backGroundImage;
		canvas.transform.GetChild (0).GetChild (7).GetComponent<Text> ().text = temp.GetComponent<SkillSet> ().skillTitle;
		canvas.transform.GetChild (0).GetChild (8).GetComponent<Text> ().text = temp.GetComponent<SkillSet> ().skillDescription;
		// setup the node to start draw the pattern.
		nextNode = temp.GetComponent<SkillSet> ().startNode;
		endNode = temp.GetComponent<SkillSet> ().endNode;
	}

	/// <summary>
	/// Changes the list of skill base. base on character being select. 
	/// </summary>
	/// <param name="newSkillSet">New skill set.</param>
	public void ChangeSKillSet (SkillSet[] newSkillSet, float mana, float newMaxMana)
	{
		skillSet = newSkillSet;
		currentMana = mana;
		maxMana = newMaxMana;
		for (int i = 0; i < 4; i++) {
			backgroundImageButtonList [i].sprite = newSkillSet [i].backGroundBtnImage;
		}
		_ChangeSkill (0);
	}

	#endregion

	#region Private Methods

	/// <summary>
	///  this function will run wheneer player start drawing patten.
	/// This will reduce stamina of the player and update on the bar.
	/// </summary>
	void ReduceMana ()
	{
		currentMana -= 0.2f;
	}

	/// <summary>
	/// Actives the skill success.
	/// This will active the skill when the patten was draw correctly
	/// </summary>
	void ActiveSkillSuccess ()
	{
		// do some skill here.

		// clear the trail, put the flag skill success, close the skill window.
		GetComponent<TrailRenderer> ().Clear ();
		skillSuccess = true;
		IsSkillScreenOpen = false;
		Debug.Log ("Finish skill");
	}

	/// <summary>
	/// Actives the skill fail.
	/// This will reset the board, as patten unccessfully drawed.
	/// </summary>
	void ActiveSkillFail ()
	{
		currentSet.GetComponent<SkillSet> ().ResetSkill (before);
		GetComponent<TrailRenderer> ().Clear ();
		Debug.Log ("Fail skill");
	}

	#endregion

}
	