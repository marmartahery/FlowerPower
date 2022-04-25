using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public enum Tooltype
    {
        Hoe, WateringCan, Axe, Pickaxe, Shovel
    }

    public Tooltype toolType;

}
