using UnityEngine;


public enum CollectableType
{
    Money,
    HealthPotion,
    ManaPotion
}

public class CollectableController : MonoBehaviour
{

    public CollectableType type = CollectableType.Money;
    public int value = 1;

    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    private bool hasBeenCollected = false;

    GameObject player;


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Show(bool show)
    {
        sprite.enabled = show;
        itemCollider.enabled = show;
        hasBeenCollected = !show;
    }

    void Collect()
    {
        Show(false);

        switch (type)
        {
            case CollectableType.Money:
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                break;

            case CollectableType.HealthPotion:
                player.GetComponent<PlayerController>().CollectHealth(value);
                break;

            case CollectableType.ManaPotion:
                player.GetComponent<PlayerController>().CollectMana(value);
                break;
        }
    }
}
