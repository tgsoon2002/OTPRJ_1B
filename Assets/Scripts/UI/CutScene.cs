using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutScene : MonoBehaviour
{

	public GameObject leftChar;
	public GameObject rightChar;
	public GameObject dialoge;

	void Awake ()
	{
		AssignGameObject ();
	}

	


	void AssignGameObject ()
	{
		leftChar = transform.GetChild (0).gameObject;
		rightChar = transform.GetChild (1).gameObject;
		dialoge = transform.GetChild (2).GetChild (0).gameObject;
		leftChar.SetActive (false);
		rightChar.SetActive (false);
	}


	public void Dialoge (bool isLeftChar, string message, string charName)
	{
		if (leftChar == null) {
			AssignGameObject ();
		}
		if (isLeftChar) {
			leftChar.SetActive (true);
			leftChar.GetComponent<Image> ().sprite = GetPortrait (charName);
			leftChar.transform.GetChild (0).GetComponentInChildren<Text> ().text = charName;
			rightChar.SetActive (false);

		} else {
			leftChar.SetActive (false);
			rightChar.SetActive (true);
			rightChar.GetComponent<Image> ().sprite = GetPortrait (charName);
			rightChar.transform.GetChild (0).GetComponentInChildren<Text> ().text = charName;
		}

		dialoge.transform.GetComponent<Text> ().text = message;
	}

	Sprite GetPortrait (string charName)
	{
		Sprite Image = (Sprite)Resources.Load (charName, typeof(Sprite));
		Debug.Log ("Resources/" + charName);
		return Image;
	}
}
