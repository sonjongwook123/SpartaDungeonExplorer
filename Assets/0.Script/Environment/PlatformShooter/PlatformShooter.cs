using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShooter : MonoBehaviour, Interactable
{
    private Rigidbody rb;
    public void Interact()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            rb.AddForce(Vector3.forward * 500, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb = null;
        }
    }
}
