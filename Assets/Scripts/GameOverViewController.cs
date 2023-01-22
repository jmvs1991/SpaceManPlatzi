using UnityEngine;
using UnityEngine.UI;

public class GameOverViewController : MonoBehaviour
{
    public Text coinsText, scoreText;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = playerController.GetTravelDistance();

            coinsText.text = $"{coins}";
            scoreText.text = $"Score: {score: 0.00}";
        }
    }
}
