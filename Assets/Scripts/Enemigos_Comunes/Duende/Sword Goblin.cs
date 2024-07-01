using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGoblin : MonoBehaviour
{
    public int damage = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamagePlayer(damage);
            }
        }
    }
}
