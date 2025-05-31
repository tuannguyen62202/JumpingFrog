using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject startGameScreen;
    public GameObject pauseMenu;
    private bool isPaused = false;
    public int currentScore = 0;

    public void StartGame()
    {
        startGameScreen.SetActive(false);
      
    }
    void Update()
    {
        // Toggle pause on ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;  // Freeze game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;  // Resume game time
        isPaused = false;
    }

    public void EndGame()
    {
        gameOverScreen.SetActive(true);
        startGameScreen.SetActive(false);
        winScreen.SetActive(false);
        Time.timeScale = 0f; // Stop time when game over

        // Update the final score
        UIManager uiManager = GetComponent<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateGameOverScoreUI(currentScore);
        }
    }

    public void WinGame()
    {
        winScreen.SetActive(true);
        startGameScreen.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void QuitGame()
    {
        // Quit the application
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Make sure time is running
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("Scene1");
    }
}
