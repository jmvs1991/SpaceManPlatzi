using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public float jumpForce = 6f;
    public float runningSpeed = 2f;

    public LayerMask groundMask;
    Rigidbody2D playerRigidBody;
    Animator animator;
    Vector3 startPossition;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startPossition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && GameManager.sharedInstance.InGame())
            Jump();

        bool isTouchingTheGround = IsTouchindTheGround();
        
        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround);

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }

    void FixedUpdate()
    {
        if (GameManager.sharedInstance.InGame())
        {
            float direction = Input.GetAxis("Horizontal");
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (Input.GetButton("Horizontal") && direction >= 0 && playerRigidBody.velocity.x < runningSpeed)
            {
                spriteRenderer.flipX = false;
                playerRigidBody.velocity = new Vector2(runningSpeed, playerRigidBody.velocity.y);
            }
            else if (Input.GetButton("Horizontal") && direction < 0 && playerRigidBody.velocity.x < runningSpeed)
            {
                spriteRenderer.flipX = true;
                playerRigidBody.velocity = new Vector2(-1 * runningSpeed, playerRigidBody.velocity.y);
            }
        }
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);

        Invoke("RestarPosition", 0.1f);
    }

    private void RestarPosition()
    {
        this.transform.position = startPossition;
        this.playerRigidBody.velocity = Vector2.zero;
    }

    void Jump()
    {
        if (IsTouchindTheGround())
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Nos indica si el personaje esta tocando o no el suelo
    /// </summary>
    /// <returns></returns>
    bool IsTouchindTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask);
    }

    public void Die()
    {
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
}
