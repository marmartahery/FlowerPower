using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public enum landStats
    {
        Soil, Farmland, Watered
    }
    public Material SoilMat, farmladMat, wateredMat;
    new Renderer renderer;

    public landStats LandStats;

    //is enable when the player is interacting with the land;
    public GameObject select;

    // Start is called before the first frame update
    void Start()
    {
        Select(false);
        //get renderer component
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(landStats.Soil);
    }

    // Update is called once per frame
    public void SwitchLandStatus(landStats statusToSwitch)
    {
        //set default land
        LandStats= statusToSwitch;
        Material materialtoSwitch = SoilMat;
        //switches the material based on what the stats is
        switch (statusToSwitch)
        {
            case landStats.Soil:
                materialtoSwitch = SoilMat;
                break;
            case landStats.Farmland:
                materialtoSwitch = farmladMat;
                break;
            case landStats.Watered:
                materialtoSwitch = wateredMat;
                break;

        }
        //Get the renderer and apply the changes
        renderer.material = materialtoSwitch;
    }
    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    //when player interacts with button
    public void interact()
    {
        //Debug.Log("Ive been interacted");
        //SwitchLandStatus(landStats.Farmland);
        //checks the player tools
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        //Try casting the itemdata in the toolSlot as Equipment Data
        EquipmentData equipmentTool = toolSlot as EquipmentData;
        //Check if it is of type EquipmentData
        if(equipmentTool != null)
        {
            //get the tool Type 
            EquipmentData.Tooltype toolType = equipmentTool.toolType;

            switch (toolType) {
                case EquipmentData.Tooltype.Hoe:
                    SwitchLandStatus(landStats.Farmland);
                    break;

                case EquipmentData.Tooltype.WateringCan:
                    SwitchLandStatus(landStats.Watered);
                    break;
            }
        }
    }
}
