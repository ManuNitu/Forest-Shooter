using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI highScore;

    private void Awake()
    {
        Cursor.visible = true;

        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);

        if (Score.score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", Score.score);

        finalScore.text = "Final Score:\n" + Score.score.ToString();
        highScore.text = "High Score:\n" + PlayerPrefs.GetInt("HighScore");
    }
    public void Retry()
    {
        Time.timeScale = 1f;

        AudioManager.Instance.PlaySfx(AudioManager.Instance.button);

        ResetVariables();

        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        AudioManager.Instance.PlaySfx(AudioManager.Instance.button);

        ResetVariables();

        SceneManager.LoadScene("MainMenu");
    }

    void ResetVariables()
    {
        SpawnEnemies.increaseHealth = 0;
        SpawnEnemies.increaseDamage = 0;

        SpawnEnemies.reduceCooldown = 0;

        SpawnEnemies.enemiesOnScreen = 0;
        SpawnEnemies.enemiesDefeated = 0;

        CoinsUI.coinsNumber = 0;

        Score.score = 0;
    }
}
