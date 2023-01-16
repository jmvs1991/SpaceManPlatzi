using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{
    public Text scoreText, coinsText, maxScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = 0f;
            float maxScore = 0f;

            coinsText.text = $"{coins}";
            scoreText.text = $"Score: {score: 0.00}";
            maxScoreText.text = $"MaxScore: {maxScore: 0.00}";
        }
    }
}
