using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollisionChecker : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            playerController.SideCollision();
            Debug.Log("check");
        }
    }
}
