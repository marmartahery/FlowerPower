using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
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

    // store when the land was watered
    GameTimestamp timeWatered;

    [Header("Crops")]
    public GameObject cropPrefab;


    CropBehavior cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        Select(false);
        //get renderer component
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(landStats.Soil);

        // start listening to TimeManager
        TimeManager.Instance.RegisterTracker(this);
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
                timeWatered = TimeManager.Instance.GetGameTimestamp();
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

        // checks if nothing is equipped
        if (toolSlot == null)
        {
            return;
        }
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

                    // can't water unless land is tilled
                    if(LandStats != landStats.Soil) {
                        SwitchLandStatus(landStats.Watered);
                    }
                    
                    break;
                case EquipmentData.Tooltype.Shovel:

                    // deletes crop from land, not stored in inventory
                    if (cropPlanted != null)
                    {
                        Destroy(cropPlanted.gameObject);
                    }
                    break;
            }

            return;
        }

        SeedData seedTool = toolSlot as SeedData;

        // conditions to plant:
        // holding SeedData, Land is watered/farmland, No crop exists on plot
        if(seedTool != null && LandStats != landStats.Soil && cropPlanted == null)
        {
            GameObject cropObject = Instantiate(cropPrefab, transform);
            // 0.526
            cropObject.transform.position = new Vector3(transform.position.x, 1.3f, transform.position.z);

            cropPlanted = cropObject.GetComponent<CropBehavior>();

            // ACTUAL PLANTING
            cropPlanted.Plant(seedTool);
        }

        
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        // number of hours that the soil stays watered = 24h
        if(LandStats == landStats.Watered)
        {
            //Debug.Log("Im here l127");
            //Debug.Log(LandStats == landStats.Watered);
            // time elapsed since land was last watered
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);
            //Debug.Log(hoursElapsed + "since last weatered");

            // GROWING THE PLANTED CROP
            //Debug.Log(cropPlanted != null);
            if(cropPlanted != null)
            {
                //Debug.Log("Im here");
                cropPlanted.Grow(); 
            }


            if(hoursElapsed > 24)
            {
                SwitchLandStatus(landStats.Farmland);
            }
        }


        // calls WITHER function
        if(LandStats != landStats.Watered && cropPlanted != null)
        {
            if(cropPlanted.cropState != CropBehavior.CropState.Seed)
            {
                cropPlanted.Wither();
            }
        }
    }
}
