using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    public Text scoreText, coinsText, maxScoreText;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController= GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = playerController.GetTravelDistance();
            float maxScore = PlayerPrefs.GetFloat("maxScore", 0f);

            coinsText.text = $"{coins}";
            scoreText.text = $"Score: {score: 0.00}";
            maxScoreText.text = $"MaxScore: {maxScore: 0.00}";
        }
    }
}
