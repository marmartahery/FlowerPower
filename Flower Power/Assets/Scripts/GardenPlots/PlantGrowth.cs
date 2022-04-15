using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{

    private int currentProgression = 0;
    public int timeBetweenGrowths;
    public int maxGrowth;
    public bool isPlanted;
    public bool isHarvested;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("entered");
        //InvokeRepeating("Growth", timeBetweenGrowths, timeBetweenGrowths); 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentProgression);
    }

    public void Growth()
    {

        //InvokeRepeating("Growth", timeBetweenGrowths, timeBetweenGrowths); // calls a function every few seconds - checks growth

        if (currentProgression != maxGrowth)
        {
            gameObject.transform.GetChild(currentProgression).gameObject.SetActive(true);
        }
        if (currentProgression > 0 && currentProgression < maxGrowth)
        {
            gameObject.transform.GetChild(currentProgression - 1).gameObject.SetActive(false);
        }
        if(currentProgression < maxGrowth)
        {
            currentProgression++;
        }
    }
}
