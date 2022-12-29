using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    public float jumpForce = 6f;
    public LayerMask groundMask;
    Rigidbody2D playerRigidBody;
    Animator animator;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
            Jump();

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchindTheGround());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
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
}
