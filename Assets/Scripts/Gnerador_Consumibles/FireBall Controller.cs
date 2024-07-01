using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : Herencia_Consumibles
{
    void Start()
    {
        speed = 10f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rbd.velocity = new Vector2(speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamagePlayer(40); 
            }
            Destroy(gameObject);
        }
    }
}