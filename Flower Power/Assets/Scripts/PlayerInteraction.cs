using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    MovementAndAnimation playercontroller;

    //highlight over the land that is selecting
    Land HighlightLand = null;

    // Start is called before the first frame update
    void Start()
    {
        //get access of the player controller
        playercontroller = transform.parent.GetComponent<MovementAndAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        //whatever the raycast hits
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OnInteractableHit(hit);
        }
    }

    //what happens when the raycast hits something interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        //checks if the player is able to interact with the land
        if (other.tag == "InteractableLand")
        {
            Debug.Log("im on interactable");
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }
        //unselect the land if the player is not on it
        if(HighlightLand != null)
        {
            HighlightLand.Select(false);
            HighlightLand = null;
        }
    }

    void SelectLand(Land land)
    {
        //set the previous land to false 
        if (HighlightLand != null)
        {
            HighlightLand.Select(false);
        }
        //Shows which land will be highlighted
        HighlightLand = land;
        land.Select(true);
    }

    //triggers when player clicks interaction key
    public void Interact()
    {
        //checks if player is on interactable land
        if(HighlightLand != null)
        {
            HighlightLand.interact();
            return;
        }
        Debug.Log("Not on interactable land");
    }
}
