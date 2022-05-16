using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Seeds")]
public class SeedData : ItemData
{
    //Time it takes before the seed matures
    public int daysToGrow;

    //the crop the seed will yeild
    public ItemData cropToYield;

    public GameObject seedling;

}
