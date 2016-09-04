using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//Must modify for Item Info Panel

public class ItemInfoPanel : MonoBehaviour 
{
    private Item item;
    public Image itemImage;
    public Text itemTitle;
    public Text itemDescription;
    public Text itemQuantity;
    //public UIManager UIManager;

    public void ChangeInfo(Item item, int quan)
    {
        //UIManager = newItemUI;

        itemTitle.text = item._ItemTitle;
        //Debug.Log(itemTitle.text);

        itemDescription.text = item._Description;
        //Debug.Log(itemDescription.text);

        //itemQuantity.text = "x" + quan.ToString();
        //Debug.Log(itemQuantity.text);

        itemImage.sprite = Resources.Load<Sprite>("Images/Icons/Items/" + item.ItemSprite) as Sprite;

    }

    public void _Click()
    {
        //this.item = GetComponent<GlobalInventory>().gameObject;
        //ChangeInfo(item);
        //UIManager.EnableBoolAnimator();

    }
}
