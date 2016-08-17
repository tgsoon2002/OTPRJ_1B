using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public string testClassType;
    public float charAttackRange;
    public string name;

    Collider2D col;
    bool isTappedBegin = false;
    bool isTappedEnd = false;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.touchCount > 0)
        {
            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    if (Input.touchCount == 1)
                    {
                        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 touchPosition = new Vector2(wp.x, wp.y);

                        RaycastHit hit;

                        col = Physics2D.OverlapPoint(touchPosition);

                        if (col != null && col.tag == "Enemy")
                        {
                            isTappedBegin = true;
                            Debug.Log(col.gameObject.name.ToString() + " was touched!!!");
                        }    
                    }
                    break;

                case TouchPhase.Ended:

                    if (isTappedBegin == true)
                    {
                        Debug.Log("Tapped that bitch!!!");
                   
                        Collider2D [] allyHit = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), charAttackRange);

                        foreach (Collider2D elt in allyHit)
                        {
                            if (elt.tag == "Player")
                            {
                                Debug.Log(elt.gameObject.GetComponent<Character>().name);
                                CharacterCombat._instance.DestroyGameObject(elt.gameObject);
                            }
                        }

                        isTappedEnd = true;
                        isTappedBegin = false;
                    }
                    break;
            }
        }
	}
}
