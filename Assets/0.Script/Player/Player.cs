using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
}