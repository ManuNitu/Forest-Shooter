using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }
    public void Play()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.button);
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.button);
        Application.Quit();
    }
}
