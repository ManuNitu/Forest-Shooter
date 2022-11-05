using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public static int coinsNumber;
    public TextMeshProUGUI coinsText;
    private void Update()
    {
        coinsText.text = coinsNumber.ToString();
    }
}
