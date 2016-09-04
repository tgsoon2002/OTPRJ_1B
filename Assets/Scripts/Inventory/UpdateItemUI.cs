using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateItemUI : MonoBehaviour 
{
    Item item;
    int quantity;
    ItemDatabase itemDatabase;
    public Image itemImage;
    public Text itemTitle;
    public Text itemDescription;
    public Text itemQuantity;
    UIManager UIManager;

    public void ChangeInfo(Item item, int quan, UIManager newItemUI)
    {
        UIManager = newItemUI;
        this.item = item;
        this.quantity = quan;

        itemTitle.text = item._ItemTitle;
        //Debug.Log(itemTitle.text);

        itemDescription.text = item._Description;
        //Debug.Log(itemDescription.text);

        itemQuantity.text = "x" + quan.ToString();
        //Debug.Log(itemQuantity.text);

        itemImage.sprite = Resources.Load<Sprite>("Images/Icons/Items/" + item.ItemSprite) as Sprite;
        //Debug.Log("image name: " + item.ItemSprite);

    }

    public void _Click()
    {
        UIManager.EnableBoolAnimator(item, quantity);
    }

    public void _DeleteItem()
    {
       // UIManager.GetComponent<Inventory>().RemoveItem(item);


    }
}
