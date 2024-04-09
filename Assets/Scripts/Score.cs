using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score;

    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = "Score:\n" + score.ToString();
    }
}
