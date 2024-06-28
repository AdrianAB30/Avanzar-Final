using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayer;
    private bool isHit;
    private float hitTimer;
    private bool isAttacking;
    private float hitAnimationDuration = 0.6f;
    private float attackAnimationDuration = 0.5f;
    private float attackTimer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public UIManager uiManager;
    public GameObject detectionArea;
    public GameObject attackArea;
    public GameObject swordGoblin;
    private bool isPlayerInRange;

    public Detected_Area detectionScript;
    public Attack_Area_Goblin attackScript;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionScript = detectionArea.GetComponent<Detected_Area>();
        attackScript = attackArea.GetComponent<Attack_Area_Goblin>();

        detectionScript.goblin_Controller = this;
        attackScript.goblin_Controller = this;
    }
    protected override void Start()
    {
        base.Start();
        speed = 3;
        isDetectedPlayer = true;
        directionX = -1;
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
                animator.SetBool("isHitGoblin", false);
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
                animator.SetBool("isAttackGoblin", false);
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

                spriteRenderer.flipX = (Target.transform.position.x < transform.position.x);

                _compRigidbody.MovePosition(Vector2.MoveTowards(_compRigidbody.position, targetPosition, speed * Time.deltaTime));

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
            animator.SetBool("isIdleGoblin", true);
            animator.SetBool("isWalkingGoblin", false);
        }
    }
    public void Walking()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingGoblin", true);
            animator.SetBool("isIdleGoblin", false);
        }
    }
    public void Attack()
    {
        if (animator != null)
        {
            animator.SetBool("isAttackGoblin", true);
            isDetectedPlayer = false;
            isAttacking = true;
            attackTimer = attackAnimationDuration;
        }
    }
    public void Death()
    {
        animator.SetTrigger("isDeathGoblin");
        isDetectedPlayer = false;
        speed = 0;
    }
    public void Hit()
    {
        animator.SetBool("isHitGoblin", true);
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
            TakeDamage(5);
        }
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
}

