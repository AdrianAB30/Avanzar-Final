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
    public float skeletonSpeed = 2.5f;
    private float attackTimer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public SFXManager sfxmanager;
    public UIManager uiManager;
    public GameObject detectionArea;
    public GameObject attackArea;
    public GameObject swordSkeleton;
    private bool isPlayerInRange;

    public DetectedAreaSkeleton detectionScript;
    public AttackAreaSkeleton attackScript;

    public SwordSkeleton swordDamage;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionScript = detectionArea.GetComponent<DetectedAreaSkeleton>();
        attackScript = attackArea.GetComponent<AttackAreaSkeleton>();

        detectionScript.skeleton_Controller = this;
        attackScript.skeletonController = this;

        swordDamage = swordSkeleton.GetComponent<SwordSkeleton>();
        swordDamage.damage = 10;
        swordDamage.enabled = false;    
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
                    FollowPlayerSkeleton();
                }
                else
                {
                    IdleSkeleton();
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
                swordDamage.enabled = false;
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
                FollowPlayerSkeleton();
            }
            else
            {
                IdleSkeleton();
            }
        }

        if (isDetectedPlayer && isPlayerInRange)
        {
            AttackSkeleton();
        }
    }
    public void FollowPlayerSkeleton()
    {
        Debug.Log("Siguiendo al jugador con el skeleton");

        Vector2 targetPosition = new Vector2(Target.transform.position.x, _compRigidbody.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_compRigidbody.position, targetPosition, skeletonSpeed * Time.fixedDeltaTime);

        _compRigidbody.MovePosition(newPosition);
        spriteRenderer.flipX = (Target.transform.position.x < transform.position.x);
        WalkingSkeleton();
    }
    public void IdleSkeleton()
    {
        if (animator != null)
        {
            animator.SetBool("isIdleSkeleton", true);
            animator.SetBool("isWalkingSkeleton", false);

            _compRigidbody.velocity = Vector2.zero;
        }
    }

    public void WalkingSkeleton()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingSkeleton", true);
            animator.SetBool("isIdleSkeleton", false);
        }
    }

    public void AttackSkeleton()
    {
        if (animator != null)
        {
            animator.SetBool("isAttackingSkeleton", true);
            sfxmanager.PlaySFX(3);
            isDetectedPlayer = false;
            isAttacking = true;
            attackTimer = attackAnimationDuration;
            swordDamage.enabled = true;
        }
    }

    public void DeathSkeleton()
    {
        animator.SetTrigger("deathSkeleton");
        sfxmanager.PlaySFX(4);
        isDetectedPlayer = false;
        skeletonSpeed = 0;
    }

    public void HitSkeleton()
    {
        animator.SetBool("hitSkeleton", true);
        isHit = true;
        hitTimer = hitAnimationDuration;
        speed = 0;
    }

    public void PlayerLostSkeleton()
    {
        isDetectedPlayer = false;
        IdleSkeleton();
        skeletonSpeed = 0;
    }

    public void PlayerDetectedSkeleton()
    {
        isDetectedPlayer = true;
        skeletonSpeed = 2.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Ataque_Flyn")
        {
            TakeDamage(damage);
        }
    }

    public override void TakeDamage(int damage)
    {
        vidaActualEnemigo -= damage;

        if (vidaActualEnemigo <= 0)
        {
            DeathSkeleton();
        }
        else
        {
            HitSkeleton();
        }
    }
}