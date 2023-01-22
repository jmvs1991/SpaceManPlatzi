using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    const string STATE_ALIVE = "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    [SerializeField]
    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15;
    public const int MAX_HEALTH = 200, MAX_MANA = 30, 
              MIN_HEALTH = 10, MIN_MANA = 0;

    public const int SUPER_JUM_COST = 5;
    public const float SUPER_JUM_FORCE = 1.5F;

    public float jumpForce = 6f;
    public float runningSpeed = 2f;
    public float jumpRaycastDistance = 1.5f;

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
        if (Input.GetButtonDown("Jump"))
            Jump(false);

        if(Input.GetButtonDown("SuperJump"))
            Jump(true);

        bool isTouchingTheGround = IsTouchindTheGround();
        
        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround);

        Debug.DrawRay(this.transform.position, Vector2.down * jumpRaycastDistance, Color.red);
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

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        Invoke("RestarPosition", 0.1f);
    }

    private void RestarPosition()
    {
        this.transform.position = startPossition;
        this.playerRigidBody.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollowController>().ResetCameraPossition();
    }

    void Jump(bool isSuperJump)
    {
        if (GameManager.sharedInstance.InGame())
        {
            if (IsTouchindTheGround())
            {
                GetComponent<AudioSource>().Play();

                float jumpForceFactor = jumpForce;

                if (isSuperJump && manaPoints >= SUPER_JUM_COST)
                {
                    manaPoints -= SUPER_JUM_COST;
                    jumpForceFactor *= SUPER_JUM_FORCE;
                }

                playerRigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            }
        }
        
    }

    /// <summary>
    /// Nos indica si el personaje esta tocando o no el suelo
    /// </summary>
    /// <returns></returns>
    bool IsTouchindTheGround()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, jumpRaycastDistance, groundMask);
    }

    public void Die()
    {
        float travelDistance = GetTravelDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxScore", 0f);

        if (travelDistance > previousMaxDistance)
            PlayerPrefs.SetFloat("maxScore", travelDistance);
        
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }

    public void CollectHealth(int points)
    {
        healthPoints += points;

        if(healthPoints > MAX_HEALTH)
            healthPoints = MAX_HEALTH;

        if (healthPoints <= 0)
            Die();
    }

    public void CollectMana(int points)
    {
        manaPoints += points;

        if (manaPoints > MAX_MANA)
            manaPoints = MAX_MANA;
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelDistance()
    {
        return this.transform.position.x - startPossition.x;
    }
}
