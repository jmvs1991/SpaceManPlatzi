using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu(bool show)
    {
        menuCanvas.enabled = show;
    }
    public void GameMenu(bool show)
    {
        gameCanvas.enabled = show;
    }

    public void GameOverMenu(bool show) 
    {
        gameOverCanvas.enabled = show;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
