using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //keeps track of item data
    ItemData itemToDisplay;

    //selects the data for the ui sprite
    public Image DisplayImage;
    //gets the sprite from item data
    public void Display(ItemData itemToDisplay)
    {
        //check if there is an item to display
        if(itemToDisplay != null)
        {
            //switch the thumbnail over
            DisplayImage.sprite = itemToDisplay.thumbnail;
            this.itemToDisplay = itemToDisplay;

            DisplayImage.gameObject.SetActive(true);
            return;
        }
        DisplayImage.gameObject.SetActive(false);
    }

    //display the item info on the info box 
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(itemToDisplay);
    }
    //resets the item info box when the player leaves
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.DisplayItemInfo(null);
    }
}
