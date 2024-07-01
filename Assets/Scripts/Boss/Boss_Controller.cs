using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Boss_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayerBoss;
    private bool isHit;
    private float hitTimer;
    private bool isAttacking;
    private float hitAnimationDurationBoss = 0.5f;
    private float attackAnimationDurationBoss = 0.5f;
    public float bossSpeed = 3f;
    private float attackTimer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public SFXManager sfxmanager;
    public UIManager uiManager;
    public GameObject detectionAreaBoss;
    public GameObject attackAreaBoss;
    private bool isPlayerInRangeBoss;

    public DetectedAreaBoss detectionScript;
    public AttackAreaBoss attackScript;

    public SwordBoss swordBoss;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionScript = detectionAreaBoss.GetComponent<DetectedAreaBoss>();
        attackScript = attackScript.GetComponent<AttackAreaBoss>();

        detectionScript.bossController = this;
        attackScript.bossControler = this;

        swordBoss = swordBoss.GetComponent<SwordBoss>();
        swordBoss.damage = 20;
        swordBoss.enabled = false;
    }

    protected override void Start()
    {
        base.Start();
        isDetectedPlayerBoss = true;
        WalkingBoss();
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
                animator.SetBool("isHitBoss", false);
                if (isDetectedPlayerBoss)
                {
                    WalkingBoss();
                }
                else
                {
                    IdleBoss();
                }
            }
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                animator.SetBool("isAttackBoss", false);
                swordBoss.enabled = false;
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isHit)
        {
            if (isDetectedPlayerBoss && Target != null)
            {
                FollowPlayerBoss();
            }
            else
            {
                IdleBoss();
            }
        }

        if (isDetectedPlayerBoss && isPlayerInRangeBoss && !isAttacking)
        {
            AttackBoss();
        }
    }

    private void FollowPlayerBoss()
    {
        Debug.Log("Siguiendo a Flyn");
        Vector2 targetPosition = new Vector2(Target.transform.position.x, _compRigidbody.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_compRigidbody.position, targetPosition, bossSpeed * Time.fixedDeltaTime);

        _compRigidbody.MovePosition(newPosition);

        if (Target.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void IdleBoss()
    {
        if (animator != null)
        {
            animator.SetBool("isIdleBoss", true);
            animator.SetBool("isRunningBoss", false);

            _compRigidbody.velocity = Vector2.zero;
        }
    }

    public void WalkingBoss()
    {
        if (animator != null)
        {
            animator.SetBool("isRunningBoss", true);
            animator.SetBool("isIdleBoss", false);
        }
    }

    public void AttackBoss()
    {
        if (animator != null)
        {
            animator.SetBool("isAttackBoss", true);
            sfxmanager.PlaySFX(7);
            isDetectedPlayerBoss = false;
            isAttacking = true;
            attackTimer = attackAnimationDurationBoss;
            swordBoss.enabled = true;
        }
    }

    public void DeathBoss()
    {
        animator.SetTrigger("isDeathBoss");
        isDetectedPlayerBoss = false;
        bossSpeed = 0;
        _compRigidbody.velocity = Vector2.zero;
        SceneManager.LoadScene("Win");
    }

    public void HitBoss()
    {
        if (animator != null)
        {
            animator.SetBool("isHitBoss", true);
            isHit = true;
            hitTimer = hitAnimationDurationBoss;
            bossSpeed = 0;
        }
    }

    public void PlayerLostBoss()
    {
        isDetectedPlayerBoss = false;
        IdleBoss();
        bossSpeed = 0;
    }

    public void PlayerDetectedBoss()
    {
        Debug.Log("Entraste al área de detección del boss");
        isDetectedPlayerBoss = true;
        bossSpeed = 3.5f;
        WalkingBoss();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Ataque_Flyn")
        {
            Debug.Log("El jefe ha sido golpeado");
            TakeDamage(20);
        }
    }

    public override void TakeDamage(int damage)
    {
        vidaActualEnemigo -= damage;
        Debug.Log("Vida del jefe: " + vidaActualEnemigo);

        if (vidaActualEnemigo <= 0)
        {
            DeathBoss();
        }
        else
        {
            HitBoss();
        }
    }
}