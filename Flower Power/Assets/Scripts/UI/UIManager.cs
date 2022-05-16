using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ITimeTracker
{
    public static UIManager Instance { get; private set; }
    [Header("Status Bar")]
    //shows the status of what item is selected
    public Image toolEquipSlot;

    // TIME AND DATE INFO
    public Text timeText;
    public Text dateText;

    [Header("Inventory System")]
    //the inventory panel
    public GameObject inventorypanel;
    //The Tool equip slot UI of the inventory panel
    public HandInventorySlot toolHandSlot;
    //the tool slots UI
    public InventorySlot[] toolSlots;
    //The item equip slot UI of the inventory panel
    public HandInventorySlot itemHandSlot;
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
        AssignSlotIndexes();

        // starts listening to time manager
        TimeManager.Instance.RegisterTracker(this);
    }

    //iterate thorugh the slot UI elements and assign it its reference
    public void AssignSlotIndexes()
    {
        for(int i=0;i<toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
            itemSlots[i].AssignIndex(i); 
        }
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

        //render equipped tool
        toolHandSlot.Display(InventoryManager.Instance.equippedTool);
        itemHandSlot.Display(InventoryManager.Instance.equippedItem);


        //Get Tool Equiped from InventoryManager
        ItemData equipped = InventoryManager.Instance.equippedTool;
        //Check if there is an item to display
        if (equipped != null)
        {
            //switch the thumbnail over
            toolEquipSlot.sprite = equipped.thumbnail;

            toolEquipSlot.gameObject.SetActive(true);
            return;
        }
        toolEquipSlot.gameObject.SetActive(false);

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

    // listens to what time it is on TimeManager using ITimeTracker
    public void ClockUpdate(GameTimestamp timestamp)
    {

        // Updating Time Text
        int hours = timestamp.hour;
        int minutes = timestamp.minute;

        string suffix = "AM ";

        if(hours > 12)
        {
            suffix = "PM ";
            hours -= 12;
        }

        timeText.text = hours + ":" + minutes.ToString("00") + " " + suffix;


        // Updating Date Text

        int day = timestamp.day;
        string season = timestamp.season.ToString();
        string dayOfTheWeek = timestamp.GetDayOfTheWeek().ToString();
        dateText.text = season + " " + day + " (" + dayOfTheWeek + ")";
    }
}
