using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Crate : MonoBehaviour
{
    public GameObject Item;
    public Transform ItemPoint;
    public int cost;
    public GameObject showCost;
    public TextMeshProUGUI amount;
    public SpriteRenderer minimapIcon;
    public Sprite MinimapIcon_Crate_Opened;
    private Animator anim;
    private bool opened;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        amount.text = cost.ToString();
        if (!showCost.activeSelf)
            return;
        if (CoinsUI.coinsNumber < cost)
            amount.color = Color.red;
        else
            amount.color = Color.white;
        if (Input.GetKey(KeyCode.E) && !opened && CoinsUI.coinsNumber >= cost)
        {
            AudioManager.Instance.PlaySfx(AudioManager.Instance.button);
            opened = true;
            CoinsUI.coinsNumber -= cost;
            anim.SetTrigger("open");
            minimapIcon.sprite = MinimapIcon_Crate_Opened;
            showCost.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened)
            return;
        if (collision.CompareTag("Player"))
        {
            showCost.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (opened)
            return;
        if (collision.CompareTag("Player"))
        {
            showCost.SetActive(false);
        }
    }
    public void SpawnItem()
    {
        Instantiate(Item, ItemPoint.transform.position, Quaternion.identity);
    }
}
