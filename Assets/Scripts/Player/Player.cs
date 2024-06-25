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
    private bool isGrounded;
    private bool canDoubleJump;
    private Rigidbody2D rbd;
    private Vector2 lastDirection;
    public Animator animator;
    public GameObject attackArea;
    public UIManager uimanager;
    public void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        lastDirection = Vector2.right;
        animator = GetComponent<Animator>();
        attackArea.SetActive(false);
    }
    public void Start()
    {
        uimanager.ActualizarBarraDeVida(vidaActualPlayer);
        vidaActualPlayer = vidaMaxPlayer;
        uimanager.SetMaxValue(vidaMaxPlayer);
    }
    private void Update()
    {
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
        MovimientoFlyn();
    }
    public void MovimientoFlyn()
    {
        Vector2 movement = new Vector2(movementHorizontal, 0);

        rbd.velocity = new Vector2(movement.x * speed , rbd.velocity.y);

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
    }
    public void Jump(float customForce)
    {
        rbd.AddForce(Vector2.up * customForce,ForceMode2D.Impulse);
        animator.SetBool("Jumping", true);
        animator.SetBool("Walking", false);
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
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            attackArea.SetActive(true);
        }
    }
    public void AttackAreaDisabled()
    {
        attackArea.SetActive(false);
    }
    public void TakeDamagePlayer(int damage)
    {
       vidaActualPlayer -= damage;
        if (vidaActualPlayer <= 0)
        {
            Debug.Log("Player ha muerto");

        uimanager.ActualizarBarraDeVida(vidaActualPlayer);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            uimanager.IncrementarMoneda();
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Gem")
        {
            uimanager.IncrementarGemas();
            Destroy(collision.gameObject);
        }
    }
}





