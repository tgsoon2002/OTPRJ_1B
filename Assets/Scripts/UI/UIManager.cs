using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    Transform cachedTransform;
    Vector3 startingPosition;
    Collider2D col;
    bool isTapped;
    Animator anim;

    float _horizontalLimit = Screen.height, _verticalLimit = Screen.width;

    private Vector2 startingTouchPosition_Screen;
    private Vector2 startingTouchPosition_World;

	// Use this for initialization
	void Start () 
    {
        col = GetComponent<BoxCollider2D>();
        cachedTransform = transform;
        startingPosition = cachedTransform.position;
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
                        startingTouchPosition_Screen = Input.GetTouch(0).position;

                        startingTouchPosition_World = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                        if (col == Physics2D.OverlapPoint(startingTouchPosition_World) && col.tag == "ItemSlot")
                        {
                            isTapped = true;
                        }
                    }
                    break;

                case TouchPhase.Moved:

                    isTapped = false;

                    break;

                case TouchPhase.Ended:

                    if (isTapped == true)
                    {
                        isTapped = false;
                        EnableBoolAnimator (anim);
                    }

                    isTapped = false;

                    break;
            }
        }
	}

    public void DisableBoolAnimator (Animator anim)
    {
        anim.SetBool("IsDisplayed", false);

    }

    public void EnableBoolAnimator (Animator anim)
    {
        anim.SetBool("IsDisplayed", true);
    }

    public void NavigateTo (int scene)
    {
        Application.LoadLevel(scene);
    }


}
