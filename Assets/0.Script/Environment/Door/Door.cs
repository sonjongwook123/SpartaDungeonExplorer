using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public Transform doorObj;

    public void Interact()
    {
        doorObj.Rotate(0, 90, 0);
    }
}
