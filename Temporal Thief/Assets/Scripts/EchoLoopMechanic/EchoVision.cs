using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVision : MonoBehaviour
{
    //how far can it see
    public float viewDistance = 5f;
    //how wide it can see
    public float viewAngle = 45f;
    //what layers it can see
    public LayerMask playerMask;
    public LayerMask objectMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        //find anything on the player layer
        Collider[] playersInRange = Physics.OverlapSphere(transform.position, viewDistance, playerMask);
        foreach (Collider player in playersInRange)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            //checik if player is inside cone
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < viewAngle)
            {
                //no objects in the way of player
                if (!Physics.Raycast(transform.position, directionToPlayer, viewDistance, objectMask));
                {
                    ParadoxManager.Instance.TriggerParadox(player.transform.position);
                }
            }
            
        }

        
    }
    //Draw gizmo to visualize the cone
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
