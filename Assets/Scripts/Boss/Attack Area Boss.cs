using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaBoss : MonoBehaviour
{
    public Boss_Controller bossControler;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player entro a ataque.");
            bossControler.AttackBoss();
        }
    }
}
