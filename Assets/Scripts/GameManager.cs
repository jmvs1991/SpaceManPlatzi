using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;
    public int collectedObject = 0;

    PlayerController playerController;

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player")
                                     .GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)
            StartGame();
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    /// <summary>
    /// 
    /// </summary>
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.GameMenu(false);
            MenuManager.sharedInstance.GameOverMenu(false);
            MenuManager.sharedInstance.MainMenu(true);
        }
        else if (newGameState == GameState.inGame)
        {
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            Invoke("ReloadLevel", 0.1f);
            MenuManager.sharedInstance.MainMenu(false);
            MenuManager.sharedInstance.GameOverMenu(false);
            MenuManager.sharedInstance.GameMenu(true);
        }
        else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.GameMenu(false);
            MenuManager.sharedInstance.MainMenu(false);
            MenuManager.sharedInstance.GameOverMenu(true);
        }

        this.currentGameState = newGameState;
    }

    public bool InGame()
    {
        return currentGameState == GameState.inGame;
    }

    private void ReloadLevel()
    {
        LevelManager.sharedInstance.GenerateInitialBlocks();
        playerController.StartGame();
    }

    public void CollectObject(CollectableController collectable)
    {
        collectedObject += collectable.value;
    }
}
