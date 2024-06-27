using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Skeleton_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayer;
    private bool isHit;
    private float hitTimer;
    private float hitAnimationDuration = 0.5f;
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

        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                isHit = false;
                animator.SetBool("hitSkeleton", false);
                if (isDetectedPlayer)
                {
                    Walking();
                }
                else
                {
                    Idle();
                }
            }
        }
        else
        {
            if (isDetectedPlayer && Target != null)
            {
                Vector2 targetPosition = new Vector2(Target.transform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                spriteRenderer.flipX = (Target.transform.position.x < transform.position.x);
                Walking();
            }
            //else
            //{
            //    Idle();   
            //}
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
            animator.SetBool("isIdleSkeleton", true);
            animator.SetBool("isWalkingSkeleton", false);
        }
    }
    public void Walking()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingSkeleton", true);
            animator.SetBool("isIdleSkeleton", false);
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
    public void Hit()
    {
        animator.SetBool("hitSkeleton", true);
        isHit = true;
        hitTimer = hitAnimationDuration;
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
            Hit();
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
            speed = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isDetectedPlayer = false;
            Idle();
            speed = 0;
        }
    }
}
