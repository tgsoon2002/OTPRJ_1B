using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    Transform cachedTransform;
    Vector3 startingPosition;
    Collider2D col;
    bool isTapped;
    public Animator anim;
    public ItemInfoPanel infoPanel;

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

    public void DisableBoolAnimator ()
    {
        anim.SetBool("IsDisplayed", false);

    }

    public void EnableBoolAnimator (Item item, int quantity)
    {
        //Debug.Log(name);
        infoPanel.ChangeInfo(item, quantity);

        anim.SetBool("IsDisplayed", true);
    }

    public void NavigateTo (int scene)
    {
        Application.LoadLevel(scene);
    }
}
