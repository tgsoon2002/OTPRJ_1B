using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingDamageText : MonoBehaviour 
{
	#region Data Members

	public Animator anim;
	private Text damageText;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void OnEnable () 
	{
		AnimatorClipInfo[] clip = anim.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, clip[0].clip.length);
		damageText = anim.gameObject.GetComponent<Text>();
	}
		
	#endregion

	#region Public Methods

	public void SetDamageValueText(float val)
	{
		//Declaring local variables
		int _val = (int)val;

		anim.GetComponent<Text>().text = _val.ToString();
	}

	#endregion

	#region Private Methods

	#endregion	
}
