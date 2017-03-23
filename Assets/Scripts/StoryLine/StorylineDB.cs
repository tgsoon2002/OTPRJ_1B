using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class StorylineDB : MonoBehaviour
{
	public GameObject cutscene;
	private JsonData storyLineJson;
	Dialouge[] storyActDB;
	public int currentLine = -1;
	public int lastLine = -1;
	public bool isCuttingScene = false;
	Ray ray;
	RaycastHit hit;
	Touch touchPos;
	public Camera cam;

	void Awake ()
	{
		ConstructData ();
	}

	// Use this for initialization
	void Start ()
	{
		ray = cam.ScreenPointToRay (new Vector3 (touchPos.position.x, touchPos.position.y, 5f));

	}
	
	// Update is called once per frame
	void Update ()
	{ 
		if (cutscene == null) { 
			cutscene = UICombatManager.Instance.StartCutScene ();
			isCuttingScene = true;
			NextLine ();
		}
	}

	void ConstructData ()
	{
		List<Dialouge> temp = new List<Dialouge> ();
		storyLineJson = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/StreamingAssets/CS_Act1.json"));
		for (int i = 0; i < storyLineJson.Count; i++) {
			
			//add new tempSKillSet with lockarray, charID, Skillpoint, qbArray.
			temp.Add (new Dialouge ((bool)storyLineJson [i] ["isLeftSide"], 
				storyLineJson [i] ["message"].ToString (),
				storyLineJson [i] ["charName"].ToString ()));
		}
		storyActDB = temp.ToArray ();
	}

	public void NextLine ()
	{
		Debug.Log ("Next !");
		currentLine++;
		if (currentLine > lastLine) {
			lastLine = currentLine;
		}
		if (currentLine < storyActDB.Length) {
			
			cutscene.GetComponent<CutScene> ().Dialoge (storyActDB [currentLine].leftSide, storyActDB [currentLine].message, storyActDB [currentLine].charName);
		} else {
			UICombatManager.Instance.EndCutScene ();
			UICombatManager.Instance._ResumeBattle ();
			isCuttingScene = false;
		}
	}

	public void ResumeLine ()
	{
		currentLine = lastLine;
		cutscene.GetComponent<CutScene> ().Dialoge (storyActDB [currentLine].leftSide, storyActDB [currentLine].message, storyActDB [currentLine].charName);

	}

	public void SkipCutsene ()
	{
		lastLine = storyActDB.Length;
		UICombatManager.Instance.EndCutScene ();
		UICombatManager.Instance._ResumeBattle ();
		isCuttingScene = false;
	}

	public void PrevLine ()
	{
		if (currentLine > 0) {
			currentLine--;
			cutscene.GetComponent<CutScene> ().Dialoge (storyActDB [currentLine].leftSide, storyActDB [currentLine].message, storyActDB [currentLine].charName);		
		}
	}
}
//(bool isLeftChar, string message, string charName, Sprite portrait)
public struct Dialouge
{
	public bool leftSide;
	public string message;
	public string charName;

	public Dialouge (bool ls, string nMessage, string nCharName)
	{
		leftSide = ls;
		message = nMessage;
		charName = nCharName;
	}

}