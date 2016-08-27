using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class testItem : MonoBehaviour 
{
    public Image itemImage;
    public Text itemTitle;
    public Text itemDescription;
    public Text itemQuantity;
    public UIManager uiManager;
   

    public void ChangeInfo(Item item, int quan)
    {
        Debug.Log(name);
        itemTitle.text = item._ItemTitle;
        Debug.Log(itemTitle.text);

        itemDescription.text = item._Description;
        Debug.Log(itemDescription.text);

        itemQuantity.text = "x" + quan.ToString();
        Debug.Log(itemQuantity.text);
    
    }

    public void _Click(){
        uiManager.EnableBoolAnimator();
    }
}
