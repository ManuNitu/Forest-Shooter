using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score;
    private void Update()
    {
        scoreText.text = "Score:\n" + score.ToString();
    }
}
