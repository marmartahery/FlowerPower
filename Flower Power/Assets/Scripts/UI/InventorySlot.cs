using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
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
}
