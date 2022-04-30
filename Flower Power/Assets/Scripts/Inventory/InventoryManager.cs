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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
