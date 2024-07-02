using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Controller : Herencia_Enemigos
{
    public bool isDetectedPlayerGoblin;
    private bool isHit;
    private float hitTimer;
    private bool isAttackingGoblin;
    private float hitAnimationDuration = 0.5f;
    private float attackAnimationDuration = 0.5f;
    public float goblinSpeed = 3f;
    private float attackTimer;
    public GameObject Target;
    private SpriteRenderer spriteRenderer;
    public SFXManager SFXManager;
    public UIManager uiManager;
    public GameObject detectionArea;
    public GameObject attackArea;
    public GameObject swordGoblin;
    private bool isPlayerInRangeGoblin;

    public Detected_Area detectionScript;
    public Attack_Area_Goblin attackScript;

    public SwordGoblin swordGoblinDamage;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionScript = detectionArea.GetComponent<Detected_Area>();
        attackScript = attackArea.GetComponent<Attack_Area_Goblin>();

        detectionScript.goblin_Controller = this;
        attackScript.goblin_Controller = this;

        swordGoblinDamage = swordGoblin.GetComponent<SwordGoblin>();
        swordGoblinDamage.damage = 15;
        swordGoblin.SetActive(false); 
    }

    protected override void Start()
    {
        base.Start();
        isDetectedPlayerGoblin = true;
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
                if (isDetectedPlayerGoblin)
                {
                    WalkingGoblin();
                }
                else
                {
                    IdleGoblin();
                }
            }
        }

        if (isAttackingGoblin)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttackingGoblin = false;
                animator.SetBool("isAttackGoblin", false);
                swordGoblin.SetActive(false);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!isHit)
        {
            if (isDetectedPlayerGoblin && Target != null)
            {
                FollowPlayerGoblin();
            }
            else
            {
                IdleGoblin();
            }
        }

        if (isDetectedPlayerGoblin && isPlayerInRangeGoblin)
        {
            AttackGoblin();
        }
    }

    private void FollowPlayerGoblin()
    {
        Debug.Log("Siguiendo a Flyn");
        Vector2 targetPosition = new Vector2(Target.transform.position.x, _compRigidbody.position.y);
        Vector2 newPosition = Vector2.MoveTowards(_compRigidbody.position, targetPosition, goblinSpeed * Time.fixedDeltaTime);

        _compRigidbody.MovePosition(newPosition);
        spriteRenderer.flipX = (Target.transform.position.x < transform.position.x);
        WalkingGoblin();
    }

    public void IdleGoblin()
    {
        if (animator != null)
        {
            animator.SetBool("isIdleGoblin", true);
            animator.SetBool("isWalkingGoblin", false);

            _compRigidbody.velocity = Vector2.zero;
        }
    }

    public void WalkingGoblin()
    {
        if (animator != null)
        {
            animator.SetBool("isWalkingGoblin", true);
            animator.SetBool("isIdleGoblin", false);
        }
    }

    public void AttackGoblin()
    {
        if (animator != null)
        {
            animator.SetBool("isAttackGoblin", true);
            SFXManager.PlaySFX(5);
            isDetectedPlayerGoblin = false;
            isAttackingGoblin = true;
            attackTimer = attackAnimationDuration;
            swordGoblin.SetActive(true); 
        }
    }

    public void DeathGoblin()
    {
        SFXManager.PlaySFX(6);
        animator.SetTrigger("isDeathGoblin");
        isDetectedPlayerGoblin = false;
        goblinSpeed = 0;
    }

    public void HitGoblin()
    {
        animator.SetBool("isHitGoblin", true);
        isHit = true;
        hitTimer = hitAnimationDuration;
        speed = 0;
    }

    public void PlayerLostGoblin()
    {
        isDetectedPlayerGoblin = false;
        IdleGoblin();
        goblinSpeed = 0;
    }

    public void PlayerDetectedGoblin()
    {
        Debug.Log("Entraste al area de deteccion del goblin");
        isDetectedPlayerGoblin = true;
        goblinSpeed = 3.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Area_Ataque_Flyn")
        {
            TakeDamage(8);
        }
    }

    public override void TakeDamage(int damage)
    {
        vidaActualEnemigo -= damage;

        if (vidaActualEnemigo <= 0)
        {
            DeathGoblin();
        }
        else
        {
            HitGoblin();
        }
    }
}