using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Controller : Herencia_Consumibles
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected virtual void Start()
    {
        speed = 4;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}

