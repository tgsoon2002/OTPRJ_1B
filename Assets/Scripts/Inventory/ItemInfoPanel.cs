using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Must modify for Item Info Panel
public class ItemInfoPanel : MonoBehaviour 
{
    public Image itemImage;
    public Text itemTitle;
    public Text itemDescription;
    public Text itemQuantity;
    UIManager UIManager;

    public void ChangeInfo(Item item, int quan, UIManager newItemUI)
    {
        UIManager = newItemUI;

        itemTitle.text = item._ItemTitle;
        //Debug.Log(itemTitle.text);

        itemDescription.text = item._Description;
        //Debug.Log(itemDescription.text);

        itemQuantity.text = "x" + quan.ToString();
        //Debug.Log(itemQuantity.text);

    }

    public void _Click()
    {
        UIManager.EnableBoolAnimator();
    }
}
