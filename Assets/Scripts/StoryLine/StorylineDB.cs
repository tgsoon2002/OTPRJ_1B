using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;

public class StorylineDB : MonoBehaviour
{
	GameObject cutscene;
	private JsonData storyLineJson;
	Dialouge[] storyActDB;
	public int count = 0;
	public bool isCuttingScene = false;

	void Awake ()
	{
		ConstructData ();
	}

	// Use this for initialization
	void Start ()
	{
		

	}
	
	// Update is called once per frame
	void Update ()
	{ 
		if (cutscene == null) { 
			cutscene = UICombatManager.Instance.StartCutScene ();
			isCuttingScene = true;
			NextLine ();
		}
		if (Input.GetTouch (0).phase == TouchPhase.Ended && isCuttingScene) {
			NextLine ();
		} 
	}

	void NextLine ()
	{
		if (count < storyActDB.Length) {
			cutscene.GetComponent<CutScene> ().Dialoge (storyActDB [count].leftSide, storyActDB [count].message, storyActDB [count].charName);
			count++;	
		} else {
			UICombatManager.Instance.EndCutScene ();
			isCuttingScene = false;
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