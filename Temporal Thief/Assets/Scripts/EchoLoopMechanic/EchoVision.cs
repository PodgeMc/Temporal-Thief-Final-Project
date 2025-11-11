using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    //how far can it see
    //how wide it can see
    //what layers it can see

    void DetectPlayer()
    {
        //find anything on the player layer
        //checik if player is inside cone
        //no objects in the way of player
    }
    //Draw gizmo to visualize the cone
    private void OnDrawGizmosSelected()
    {
        
    }
}
