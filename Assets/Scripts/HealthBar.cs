using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float maxHp;
    public float currentHp;
    public Image healthBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        healthBar.fillAmount = (currentHp / maxHp) / 2;

	}
}
