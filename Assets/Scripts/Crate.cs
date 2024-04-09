using UnityEngine;
using TMPro;

public class Crate : MonoBehaviour
{
    public int cost;
    public TextMeshProUGUI costText;
    public GameObject showCost;

    public SpriteRenderer minimapIcon;
    public Sprite MinimapIcon_Crate_Opened;

    public Transform ItemPoint;
    public GameObject Item;


    private bool opened;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        costText.text = cost.ToString();

        if (!showCost.activeSelf)
            return;

        if (CoinsUI.coinsNumber < cost)
            costText.color = Color.red;
        else
            costText.color = Color.white;

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
            showCost.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (opened)
            return;

        if (collision.CompareTag("Player"))
            showCost.SetActive(false);
    }

    public void SpawnItem()
    {
        Instantiate(Item, ItemPoint.transform.position, Quaternion.identity);
    }
}
