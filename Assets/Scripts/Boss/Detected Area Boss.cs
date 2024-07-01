using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedAreaBoss : MonoBehaviour
{
    public Boss_Controller bossController;

    public void SetBoss(Boss_Controller boss)
    {
        bossController = boss;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossController.PlayerDetectedBoss();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossController.PlayerLostBoss();
        }
    }

}
