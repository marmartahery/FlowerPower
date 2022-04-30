using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("Inventory System")]
    //the inventory panel
    public GameObject inventorypanel;
    //the tool slots UI
    public InventorySlot[] toolSlots;
    //the item slot UI
    public InventorySlot[] itemSlots;
    //Item info box
    public Text itemNameText;
    


    public void Awake()
    {
        //if there is more than one instance, destroy the extra
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        RenderInventory();
    }
    //render the inventory screen to reflect the player's inventory.
    public void RenderInventory()
    {
        ItemData[] inventorytoolSlots = InventoryManager.Instance.tools;
        //activate for each item
        //get the inventory slots from inventory manager
        ItemData[] inventoryitemSlots = InventoryManager.Instance.items;
        //render the tool section
        renderInventoryPanel(inventorytoolSlots, toolSlots);
        //render the item section
        renderInventoryPanel(inventoryitemSlots, itemSlots);
    }
    //iterate through a slot in a section and display them in the UI
    void renderInventoryPanel(ItemData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void ToggleInventoryPanel()
    {
        //if the panel is hidden, show it and vice vers
        inventorypanel.SetActive(!inventorypanel.activeSelf);
        RenderInventory();
    }
    //Display item info and the iten infoBox
    public void DisplayItemInfo(ItemData data)
    {
        //if data is null; reset
        if(data == null)
        {
            itemNameText.text = "";
            //itemDescription.text=""
            return;
        }
        itemNameText.text = data.name;
    }
}
