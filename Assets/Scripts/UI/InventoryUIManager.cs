using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    Transform cachedTransform;
    Vector3 startingPosition;
    Collider2D col;
    bool isTapped;
    public Animator infoPanelAnim;
    public Animator useItemPanelAnim;
    public ItemInfoPanel infoPanel;
    private bool isInCombat;
    Inventory inventory;

    float _horizontalLimit = Screen.height, _verticalLimit = Screen.width;

    private Vector2 startingTouchPosition_Screen;
    private Vector2 startingTouchPosition_World;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        cachedTransform = transform;
        startingPosition = cachedTransform.position;
    }

    public void DisableBoolAnimator()
    {
        //infoPanelAnim.SetBool("IsDisplayed", false);
        if (useItemPanelAnim.GetBool("IsDisplayed") == true)
        {
            useItemPanelAnim.SetBool("IsDisplayed", false);
        }

        if (infoPanelAnim.GetBool("IsDisplayed") == true)
        {
            infoPanelAnim.SetBool("IsDisplayed", false);
        }

    }

    public void EnableBoolAnimator(Item item, int quantity)
    {
        Debug.Log("Trying to show right panel.");

        //isInCombat = GetComponent<Inventory>().Is_Combat_Mode;
        ////Debug.Log(name);
        //if (isInCombat)
        //{
        //    useItemPanelAnim.SetBool("IsDisplayed", true);
        //}
        //else if (!isInCombat)
        //{
        //    infoPanel.ChangeInfo(item, quantity);

        //    infoPanelAnim.SetBool("IsDisplayed", true);
        //}



        //infoPanel.ChangeInfo(item, quantity);

        //infoPanelAnim.SetBool("IsDisplayed", true);
    }

    public void NavigateTo(int scene)
    {
        Application.LoadLevel(scene);
    }
}
