using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedAreaSkeleton : MonoBehaviour
{
    public Skeleton_Controller skeleton_Controller;

    public void SetSkeleton(Skeleton_Controller skeleton)
    {
        skeleton_Controller = skeleton;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            skeleton_Controller.PlayerDetected();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            skeleton_Controller.PlayerLost();
        }
    }
}
