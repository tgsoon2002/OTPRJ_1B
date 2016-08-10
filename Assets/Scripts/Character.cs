using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
	public string testClassType;
	public float charAttackRange;
	public string name;
	public bool isSeleted;

	public bool IsSeleted {
		get{ return  isSeleted; }
		set {
			isSeleted = value; 
			if (value) {
				this.GetComponent<MeshRenderer> ().material.SetFloat ("_Outline", 0.003f);
			} else {
				this.GetComponent<MeshRenderer> ().material.SetFloat ("_Outline", 0.0f);	
			}

		}
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Selected ()
	{
		if (IsSeleted) {
			IsSeleted = false;	
		} else {
			IsSeleted = true;
		}
	}

}
