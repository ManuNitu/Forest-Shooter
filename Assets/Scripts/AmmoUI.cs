using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public int ammo;
    public int currentAmmo;
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = currentAmmo.ToString() + "/" + ammo.ToString();
    }
}
