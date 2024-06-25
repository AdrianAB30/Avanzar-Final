using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Herencia_Enemigos : MonoBehaviour
{
    public int vidaMaxEnemigo = 100;
    public int vidaActualEnemigo;
    public float speed;
    public float directionX = 1f;
    public int damage = 20;
    protected Rigidbody2D _compRigidbody;
    protected SpriteRenderer _compSpriteRenderer;
    protected Animator animator;
    protected Player player;

    protected virtual void Awake()
    {
        _compRigidbody = GetComponent<Rigidbody2D>();
        _compSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        vidaActualEnemigo = vidaMaxEnemigo;
    }
    protected virtual void Update()
    {
        if (vidaActualEnemigo <= 0)
        {
            Destroy(gameObject, 1.2f);
        }
    }
    protected virtual void FixedUpdate()
    {
        _compRigidbody.velocity = new Vector2(directionX * speed, _compRigidbody.velocity.y);
    }
    public virtual void TakeDamage(int damage)
    {
        vidaActualEnemigo = vidaActualEnemigo - damage;
        if (vidaActualEnemigo < 0)
        {
            vidaActualEnemigo = 0;
        }
    }
    public virtual void CauseDamage(int damageCaused)
    {
        
    }
}