using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehavior : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Stages of Life")]
    public GameObject seed;
    public GameObject wilted;
    private GameObject seedling;
    private GameObject harvestable;

    //int growth;
    int growth = 4320;
    // as of rn our max growth is 3 for every plant
    int maxGrowth;

    int maxHealth = GameTimestamp.HoursToMinutes(48);
    int health;

    public enum CropState
    {
        Seed, Seedling, Harvestable, Wilted
    }

    // current STATE
    public CropState cropState;

    
    // gets called when player plants a seed
    public void Plant(SeedData seedToGrow)
    {
        this.seedToGrow = seedToGrow;

        // instantiate seedling and gameobjects
        seedling = Instantiate(seedToGrow.seedling, transform);

        // crop data
        ItemData cropToYield = seedToGrow.cropToYield;

        // harvestable crop
        harvestable = Instantiate(cropToYield.gameModel, transform);

        // converting to hours
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        // converting hours to mins
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        // initializes seed stage
        SwitchState(CropState.Seed);

    }

    public void Grow()
    {
        //Debug.Log("grow function");
        

        //Debug.Log(maxGrowth);
        // germination TIME
        //growth = 4300;
        growth++;

        if(health < maxHealth)
        {
            health++;
        }
        //Debug.Log(growth);
        if (growth >= maxGrowth / 2 && cropState == CropState.Seed)
        {
            //Debug.Log("TRUE TRUWEUTER");
            SwitchState(CropState.Seedling);
        }

        //Debug.Log(cropState);
        // fully grown - last state reached 
        if(growth >= maxGrowth && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Harvestable);

        }
    }

    // WITHER SYSTEM
    public void Wither()
    {
        health--;
        if(health <= 0 && cropState != CropState.Seed)
        {
            SwitchState(CropState.Wilted);
        }
    }
    // changes the crop's state
    // e.g. dirt -> seedling -> plant 
    public void SwitchState(CropState stateToSwitch)
    {
        // turns off all the states, so we can set to one 
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestable.SetActive(false);
        wilted.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                seedling.SetActive(true);

                // seedling health initialized
                health = maxHealth;

                break;
            case CropState.Harvestable:
                harvestable.SetActive(true); 
                harvestable.transform.parent = null;
                Destroy(gameObject);
                break;
            case CropState.Wilted:
                wilted.SetActive(true);
                break;

        }
        cropState = stateToSwitch;
    }
}
