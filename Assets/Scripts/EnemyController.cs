using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool facingRight = false;
    public float runningSpeed = 3f;
    public int enemyDamage = 10;
    Rigidbody2D rigidBody;
    Vector3 startPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    void Start()
    {
        this.transform.position = startPosition;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
         float currentRunningSpeed = runningSpeed;

        if (facingRight)
        {
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.sharedInstance.InGame()) 
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Coin") || collision.CompareTag("Potion"))
        {
            return;
        }
        else if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }

        facingRight = !facingRight;
    }
}
