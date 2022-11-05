using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Inventory : MonoBehaviour
{
    public List<Image> WeaponsUI;
    public List <Gun> Weapons;
    public Sprite selected;
    public Sprite notSelected;
    private int weaponIndex = 0;
    public bool shooting;
    public bool reloading;
    private void Update()
    {
        if (shooting)
            return;

        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number-1 >= 0 && number-1 < Weapons.Count)
                ChangeWeapon(number-1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weaponIndex == Weapons.Count - 1)
                ChangeWeapon(0);
            else
                ChangeWeapon(weaponIndex + 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weaponIndex == 0)
                ChangeWeapon(Weapons.Count - 1);
            else
                ChangeWeapon(weaponIndex - 1);
        }
    }
    public void ChangeWeapon(int index)
    {
        if (weaponIndex != index)
        {
            Weapons[weaponIndex].gameObject.SetActive(false);
            WeaponsUI[weaponIndex].sprite = notSelected;
            weaponIndex = index;
            Weapons[index].gameObject.SetActive(true);
            WeaponsUI[index].sprite = selected;
        }
    }
}
