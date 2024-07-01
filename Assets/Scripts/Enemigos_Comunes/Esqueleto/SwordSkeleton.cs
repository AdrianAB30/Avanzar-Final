using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkeleton : MonoBehaviour
{
    public int damage = 10;

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
