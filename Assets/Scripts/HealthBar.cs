using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{

	private float barValue;

	public float BarValue {
		get{ return  barValue; }
		set {
			barValue = value;
			this.GetComponent<SpriteRenderer> ().material.SetFloat ("_Cutoff", 1 - barValue);
		}
	}


}
