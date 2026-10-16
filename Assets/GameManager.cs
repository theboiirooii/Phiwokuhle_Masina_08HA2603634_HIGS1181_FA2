using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Global: shows the current score on screen
    public TextMeshProUGUI scoreText;
    // Global: shows the current health on screen
    public TextMeshProUGUI healthText;
    // Global: "You Win" object, hidden until the player wins
    public GameObject winText;
    // Global: "Game Over" object, hidden until health reaches 0
    public GameObject gameOverText;

    private int score = 0;
    private int health = 3;
    public bool isGameOver = false;

    void Start()
    {
        UpdateUI();
    }

    public int AddScore()
    {
        score++;
        UpdateUI();
        if (CheckWinCondition())
        {
            isGameOver = true;
            winText.SetActive(true);
            Debug.Log("Win condition triggered");
        }
        return score;
    }

    public void ReduceHealth()
    {
        health--;
        UpdateUI();
        if (CheckLoseCondition())
        {
            isGameOver = true;
            gameOverText.SetActive(true);
        }
    }

    public bool CheckWinCondition() { return score == 5; }

    public bool CheckLoseCondition() { return health <= 0; }

    public void UpdateUI()
    {
        scoreText.text = "Score: " + score + " / 5";
        healthText.text = "Health: " + health + " / 3";
    }
}