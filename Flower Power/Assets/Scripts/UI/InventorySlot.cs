using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //keeps track of item data
    ItemData itemToDisplay;

    //selects the data for the ui sprite
    public Image DisplayImage;

    public enum InventoryType
    {
        Item, Tool
    }
    //determines which incentory section this slot is
    public InventoryType inventoryType;

    int slotIndex;


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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        //move from inventory to hand
        InventoryManager.Instance.InventoryToHand(slotIndex ,inventoryType);
    }

    //set slot index to know which slot is which
    public void AssignIndex(int slotIndex)
    {
        this.slotIndex = slotIndex;
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
