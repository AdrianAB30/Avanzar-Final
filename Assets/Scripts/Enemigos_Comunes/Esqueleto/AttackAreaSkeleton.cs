using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaSkeleton : MonoBehaviour
{
    public Skeleton_Controller skeletonController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player entro a ataque.");
            skeletonController.AttackSkeleton();
        }
    }
}
