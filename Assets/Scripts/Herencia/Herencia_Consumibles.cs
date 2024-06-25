using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herencia_Consumibles : MonoBehaviour
{
    protected Rigidbody2D rbd;
    protected float speed = 2f;

    protected virtual void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
    }
    protected virtual void FixedUpdate()
    {
        rbd.velocity = new Vector2(0, speed);
    }
}