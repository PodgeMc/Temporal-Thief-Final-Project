using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PressurePlate : MonoBehaviour
{
    public DoorController door; // the door this plate controls
    private Vector3 startScale; // original size of the plate
    private int pressCount = 0; // how many things are standing on it

    void Awake()
    {
        startScale = transform.localScale; // save starting size
        GetComponent<Collider>().isTrigger = true; // make collider a trigger (so it detects but doesn’t block)
    }

    void OnTriggerEnter(Collider other)
    {
        pressCount++; // something stepped on the plate
        door.SetOpen(true); // tell the door to open
        transform.localScale = startScale + new Vector3(0, -0.02f, 0); // squash plate a little
        Debug.Log("collided with pressure plate");
    }

    void OnTriggerExit(Collider other)
    {
        pressCount--; // something left the plate
        if (pressCount <= 0) // if nothing is left pressing
        {
            pressCount = 0; // keep safe (no negatives)
            door.SetOpen(false); // tell the door to close
            transform.localScale = startScale; // put plate back to normal size
            Debug.Log("stepped off pressure plate");
        }
    }
}
