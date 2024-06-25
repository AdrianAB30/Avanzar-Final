using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public UIManager uiManager;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        speed = 3;
        base.Start();
        Walking();
    }
    protected override void Update()
    {
        base.Update();   
        if (isDetectedPlayer && Target != null)
        {
            Vector2 targetPosition = new Vector2(Target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Target.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (Target.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            Walking();
        }
        else
        {
            Idle();
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void Idle()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingSkeleton", false);
        }
    }
    public void Walking()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingSkeleton", true);
        }
        else
        {
            Debug.Log("Estas fuera de rango");
        }
    }
    public void Attack()
    {
        if (animator != null)
        {
            animator.SetBool("isAttackSkeleton", true);
            CauseDamageToPlayer();
        }
    }
    public void Death()
    {
        animator.SetTrigger("deathSkeleton");
        isDetectedPlayer = false;
        speed = 0;      
    }
    public override void TakeDamage(int damage)
    {
        vidaActualEnemigo -= damage;

        if (vidaActualEnemigo <= 0)
        {
            Death();     
        }
        else
        {
            animator.SetBool("hitSkeleton", true);
        }    
    }
    public void CauseDamageToPlayer()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Area_Ataque_Flyn")
        {
            TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Player")
        {
            isDetectedPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDetectedPlayer = false;
        }
    }
}
