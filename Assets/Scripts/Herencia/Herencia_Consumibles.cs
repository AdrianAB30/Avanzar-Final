using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herencia_Consumibles : MonoBehaviour
{
    protected Rigidbody2D rbd;
    protected float speed;

    protected virtual void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
    }
    protected virtual void FixedUpdate()
    {
        rbd.velocity = new Vector2(0, -speed);

        if (transform.position.y <= -12)
        {
            Destroy(gameObject);
        }
    }
}