using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("Inventory System")]
    public InventorySlot[] toolSlots;
    public InventorySlot[] itemSlots;
    public void Awake()
    {
        //if there is more than one instance, destroy the extra
        if (Instance == null && Instance != this)
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
}
