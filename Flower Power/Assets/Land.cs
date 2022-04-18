using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{

    //is enable when the player is interacting with the land;
    public GameObject select;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }
}
