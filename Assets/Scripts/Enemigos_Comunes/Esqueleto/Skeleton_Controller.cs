using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayer;
    private bool isHit;
    private float hitTimer;
    private bool isAttacking;
    private float hitAnimationDuration = 0.5f;
    private float attackAnimationDuration = 0.5f;
    private float attackTimer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public UIManager uiManager;
    public GameObject detectionArea;
    public GameObject attackArea;
    public GameObject swordSkeleton;    
    private bool isPlayerInRange;

    public DetectedAreaSkeleton detectionScript;
    public AttackAreaSkeleton attackScript;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionScript = detectionArea.GetComponent<DetectedAreaSkeleton>();
        attackScript = attackArea.GetComponent<AttackAreaSkeleton>();

        detectionScript.skeleton_Controller = this;
        attackScript.skeletonController = this;
    }
    protected override void Start()
    {
        base.Start();
        isDetectedPlayer = true;
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
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                animator.SetBool("isAttackingSkeleton", false);
            }
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isHit)
        {
            if (isDetectedPlayer && Target != null)
            {
                Vector2 targetPosition = new Vector2(Target.transform.position.x, _compRigidbody.position.y);

                _compRigidbody.MovePosition(Vector2.MoveTowards(_compRigidbody.position, targetPosition, speed * Time.deltaTime));

                Vector2 direction = (targetPosition - (Vector2)_compRigidbody.position).normalized;
                spriteRenderer.flipX = (Target.transform.position.x < transform.position.x);

                Walking();
            }
            else
            {
                Idle();
            }
        }
        if (isDetectedPlayer && isPlayerInRange)
        {
            Attack();
        }
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
            animator.SetBool("isAttackingSkeleton", true);
            isDetectedPlayer = false;
            isAttacking = true;
            attackTimer = attackAnimationDuration;
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
    public void PlayerLost()
    {
        isDetectedPlayer = false;
        Idle();
        speed = 0;
    }
    public void PlayerDetected()
    {
        isDetectedPlayer = true;
        speed = 2.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Ataque_Flyn")
        {
            TakeDamage(damage);
        }    
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "EspadaSkeleton" && isAttacking)
    //    {
    //        Player player = collision.gameObject.GetComponent<Player>();
    //        if (player != null)
    //        {
    //            player.TakeDamagePlayer(10);
    //            uiManager.CambiarVidaActual(player.vidaActualPlayer);
    //        }
    //    }
    //}
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
}