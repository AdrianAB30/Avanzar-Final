using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Area_Goblin : MonoBehaviour
{
    public Goblin_Controller goblin_Controller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player entro a ataque de Goblin.");
            goblin_Controller.AttackGoblin();
        }
    }
}
