using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door; // the cube that will move like a door
    public Vector3 openOffset = new Vector3(0, 2.2f, 0); // how far the door moves when opening
    public float speed = 4f; // how fast the door moves

    private Vector3 closedPos; // where the door starts
    private Vector3 openPos;   // where the door goes when open
    private bool isOpen = false; // is the door open right now?

    void Awake()
    {
        closedPos = door.position; // save starting spot
        openPos = closedPos + openOffset; // figure out where "open" is
    }

    void Update()
    {
        Vector3 target = isOpen ? openPos : closedPos; // pick open or closed target
        door.position = Vector3.Lerp(door.position, target, Time.deltaTime * speed); // smoothly move door
    }

    public void SetOpen(bool open) => isOpen = open; // call this to open or close door
}
