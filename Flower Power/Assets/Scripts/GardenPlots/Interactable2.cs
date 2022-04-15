using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable2 : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity))
        {
            var obj = hit.collider.gameObject;

            if (hit.distance <= 1.6f && obj.name == "Plot2")
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
        }
        else
        {
            isInRange = false;
        }

        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }


}
