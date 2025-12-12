using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVision : MonoBehaviour
{
    public float viewDistance = 5f; // how far the echo can see
    public float viewAngle = 45f;   // how wide the echo can see

    public LayerMask playerMask;      // layer that the player is on
    public LayerMask obstructionMask; // layer for walls/objects that block vision

    void Update()
    {
        DetectPlayer(); // check every frame if we can see the player
    }

    void DetectPlayer()
    {
        // find anything on the player layer within the vision distance
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewDistance, playerMask);

        foreach (Collider player in playersInRange) // go through every player we found
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized; // direction from echo to player

            float angle = Vector3.Angle(transform.forward, directionToPlayer); // angle between where echo looks and the player

            if (angle < viewAngle) // if player is inside the cone
            {
                // check if there is something blocking the view between echo and player
                if (!Physics.Raycast(transform.position, directionToPlayer, viewDistance, obstructionMask)) // no wall in the way
                {
                    ParadoxManager.Instance.TriggerParadox(player.transform.position); // trigger paradox because echo saw the player
                }
            }
        }
    }

    // Draw gizmo to visualize the vision range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // make gizmo yellow
        Gizmos.DrawWireSphere(transform.position, viewDistance); // draw circle showing how far we see
    }
}
