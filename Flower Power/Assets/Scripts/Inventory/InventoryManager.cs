using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public void Awake()
    {
        //if there is more than one instance, destroy the extra
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Tools")]
    // The set oof tools
    public ItemData[] tools = new ItemData[6];
    //tool in the player's hand
    public ItemData equippedTool = null;
    [Header("Items")]
    //Item slots
    public ItemData[] items = new ItemData[6];
    //Item in the player's hand
    public ItemData equippedItem = null;

    public Transform handPoint;


    //Equip

    //Handles movement of the item from inventory to Hand
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //cache the inventory slot itemdata from inventory manager
            ItemData itemToEquip = items[slotIndex];
            //change the inventory slkot to the hand
            items[slotIndex] = equippedItem;
            //change the hand slot to the inventory slot
            equippedItem = itemToEquip;

        }
        else
        {
            //cache the inventory slot itemdata from inventory manager
            ItemData toolToEquip = tools[slotIndex];
            //change the inventory slkot to the hand
            tools[slotIndex] = equippedTool;
            //change the hand slot to the inventory slot
            equippedTool = toolToEquip;
        }
        //update the changes to the UI
        UIManager.Instance.RenderInventory();
    }

    //Handles Movement of item from Hand to inventory
    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Item)
        {
            //iterate through each inventory slotn nand find the empoty sloit
            for(int i =0; i< items.Length; i++)
            {
                if(items[i] == null)
                {
                    //send the item over to the empty space
                    items[i] = equippedItem;
                    //remove item from hand
                    equippedItem = null;
                    break;
                }
                
            }

    
        }
        else
        {
            //iterate through each inventory slotn nand find the empoty sloit
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    //send the item over to the empty space
                    tools[i] = equippedTool;
                    //remove item from hand
                    equippedTool = null;
                    break;
                }

            }
        }
        //update changes in inventory
        UIManager.Instance.RenderInventory();

    }

    // Equipped item renderer

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
