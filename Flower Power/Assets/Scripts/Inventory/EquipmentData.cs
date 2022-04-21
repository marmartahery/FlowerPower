using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public enum Tooltype
    {
        Hoe, WateringCan, Axe, Pickaxe
    }

    public Tooltype toolType;

}
