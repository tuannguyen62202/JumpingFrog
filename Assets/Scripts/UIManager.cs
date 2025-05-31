using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI score;
    private TextMeshProUGUI time;
    private Image playerHealthFillImage;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    void Start()
    {
        score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        time = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();
        playerHealthFillImage = GameObject.Find("HealthBarFill").GetComponent<Image>();
    }

    void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        time.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    public void UpdateScoreUI(int newScore)
    {
        if (score != null)
        {
            score.text = "Score: " + newScore;
        }
    }
    
    public void UpdateHealthUI(float newHealth)
    {
        if (playerHealthFillImage != null)
        {
            playerHealthFillImage.fillAmount = newHealth;
        }
    }

    public void UpdateGameOverScoreUI(int finalScore)
    {
        if (gameOverScoreText != null)
        {
            gameOverScoreText.text = "Final Score: " + finalScore;
        }
    }
}
