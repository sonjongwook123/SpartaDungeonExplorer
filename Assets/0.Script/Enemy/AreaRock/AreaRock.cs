using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRock : MonoBehaviour
{
    [Header("Attack Area Settings")]
    public LayerMask playerLayer;

    void Start()
    {
        InvokeRepeating("CheckForPlayersInArea", 0f, 1);
    }

    void CheckForPlayersInArea()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<CircleAreaDisplay>().GetCircleRadius(), playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            Player player = hitCollider.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(10);
                player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * 450, ForceMode.Impulse);
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
                // break;
            }
        }
    }
}
