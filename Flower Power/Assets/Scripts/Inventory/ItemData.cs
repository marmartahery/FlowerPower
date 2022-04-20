using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Item")]
public class ItemData : ScriptableObject
{
    public string Description;

    //icon to display
    public Sprite thumbnail;

    public GameObject gameMode1;
}
