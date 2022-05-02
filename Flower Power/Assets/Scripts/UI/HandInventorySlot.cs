using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandInventorySlot : InventorySlot
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        //Move itrm from hand to inventory
        InventoryManager.Instance.HandToInventory(inventoryType);
    }
}
