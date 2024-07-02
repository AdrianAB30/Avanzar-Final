using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int vidaMaxPlayer = 100;
    public int vidaActualPlayer;
    public float speed = 3f;
    public float movementHorizontal;
    public float forceJump = 2f;
    public float doubleJumpForce = 1f;
    private bool isAlive = true;
    private bool isGrounded;
    private bool canDoubleJump;
    private Rigidbody2D rbd;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;
    public Animator animator;
    public GameObject attackArea;
    public UIManager uimanager;
    public SFXManager sfxmanager;

    private float damageColorDuration = 0.3f; 
    private float damageColorTimer = 0f; 
    private bool isDamaged = false;

    public void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        lastDirection = Vector2.right;
        animator = GetComponent<Animator>();
        attackArea.SetActive(false);
    }

    public void Start()
    {
        if (PlayerPrefs.HasKey("PlayerLife"))
        {
            vidaActualPlayer = PlayerPrefs.GetInt("PlayerLife");
        }
        else
        {
            vidaActualPlayer = vidaMaxPlayer;
            PlayerPrefs.SetInt("PlayerLife", vidaMaxPlayer); 
        }
        uimanager.CambiarVidaActual(vidaActualPlayer);
    }
    public void ReiniciarVidaJuego()
    {
        PlayerPrefs.DeleteKey("PlayerLife");
        vidaActualPlayer = vidaMaxPlayer;
        PlayerPrefs.SetInt("PlayerLife", vidaMaxPlayer); 
        uimanager.CambiarVidaActual(vidaActualPlayer);
    }

    private void Update()
    {
        if (!isAlive) return;

        if (isDamaged)
        {
            damageColorTimer -= Time.deltaTime;
            if (damageColorTimer <= 0f)
            {
                spriteRenderer.color = Color.white; 
                isDamaged = false;
            }
        }

        movementHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                isGrounded = false;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
            }
        }
        UpdateSpriteDirection();
        FlynAttack();
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        MovimientoFlyn();
    }

    public void MovimientoFlyn()
    {
        Vector2 movement = new Vector2(movementHorizontal, 0);

        rbd.velocity = new Vector2(movement.x * speed, rbd.velocity.y);

        if (movement != Vector2.zero)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", true);
            lastDirection = movement;
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walking", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("Walking", rbd.velocity.x != 0);
            animator.SetBool("Jumping", false);
        }
    }

    public void Jump()
    {
        rbd.AddForce(Vector2.up * forceJump, ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
        animator.SetBool("Walking", false);
        sfxmanager.PlaySFX(1);
    }

    public void Jump(float customForce)
    {
        rbd.AddForce(Vector2.up * customForce, ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
        animator.SetBool("Walking", false);
        sfxmanager.PlaySFX(1);
    }

    public void UpdateSpriteDirection()
    {
        Vector3 scale = transform.localScale;
        if (lastDirection.x > 0 && scale.x < 0)
        {
            scale.x *= -1;
        }
        else if (lastDirection.x < 0 && scale.x > 0)
        {
            scale.x *= -1;
        }
        if (lastDirection.y > 0 && scale.y < 0)
        {
            scale.y *= -1;
        }
        else if (lastDirection.y < 0 && scale.y > 0)
        {
            scale.y *= 1;
        }
        transform.localScale = scale;
    }

    public void FlynAttack()
    {
        if (!isAlive) return;
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            attackArea.SetActive(true);
            sfxmanager.PlaySFX(0);
        }
    }

    public void FlynDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
            speed = 0f;
            isAlive = false;
            sfxmanager.PlaySFX(2);
            Destroy(gameObject, 2.5f);
            SceneManager.LoadScene("Game_Over");
        }
    }

    public void AttackAreaDisabled()
    {
        attackArea.SetActive(false);
    }

    public void TakeDamagePlayer(int damage)
    {
        if (!isAlive) return;
        vidaActualPlayer -= damage;
        uimanager.CambiarVidaActual(vidaActualPlayer);

        spriteRenderer.color = Color.red; 
        damageColorTimer = damageColorDuration;
        isDamaged = true;

        if (vidaActualPlayer <= 0)
        {
            FlynDeath();
            Debug.Log("Player ha muerto");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FlechaContinuar")
        { 
            PlayerPrefs.SetInt("PlayerLife", vidaActualPlayer);
            SceneManager.LoadScene("Nivel2");
        }
        if (!isAlive) return;
        if (collision.gameObject.tag == "Coin")
        {
            sfxmanager.PlaySFX(7);
            uimanager.IncrementarMoneda();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Gem")
        {
            sfxmanager.PlaySFX(7);
            uimanager.IncrementarGemas();
            Destroy(collision.gameObject);
        }
    }
    private void OnDisable()
    {
        //PlayerPrefs.DeleteKey("PlayerLife");
    }
}