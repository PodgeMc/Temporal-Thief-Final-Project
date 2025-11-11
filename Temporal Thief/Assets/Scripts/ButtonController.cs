using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ButtonController : MonoBehaviour
{
    public DoorController door; // the door this button controls
    private Vector3 startPos; // where the button starts
    private bool isPressed = false; // if the button is being pressed

    void Awake()
    {
        startPos = transform.localPosition; // save the starting position
        GetComponent<Collider>().isTrigger = true; // button should detect, not block
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            isPressed = true; // mark it as pressed
            transform.localPosition = startPos + new Vector3(0, -0.05f, 0); // move it down a bit
            door.SetOpen(true); // open the door
            Debug.Log("Button pressed!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        isPressed = false; // button released
        transform.localPosition = startPos; // move it back up
        door.SetOpen(false); // close the door
        Debug.Log("Button released!");
    }
}