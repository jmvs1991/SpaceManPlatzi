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
    PlayerController playerController;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }    
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
            //TODO: colocar la logica del menu
        }
        else if (newGameState == GameState.inGame)
        {
            playerController.StartGame();
            //TODO: preparar la escena
        }
        else if (newGameState == GameState.gameOver)
        {
            //TODO: 
        }

        this.currentGameState = newGameState;
    }

    public bool InGame()
    {
        return currentGameState == GameState.inGame;
    }
}
