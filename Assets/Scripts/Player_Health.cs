using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player_Health : MonoBehaviour
{
    public float Hp;
    public float shakeCamForce;
    public float shakeCamDuration;
    public float hurtDuration;
    public Slider heathBar;
    private Material normal;
    public Material flash;
    private float currentHp;
    private int layerOrder;
    private SpriteRenderer sprite;
    private Animator anim;
    private Player_Move playerMove;
    private Inventory inventory;
    public GameObject GameOver;
    public float invincibilityTime;
    private bool invincible;
    private bool died;
    private void Awake()
    {
        
        heathBar.maxValue = Hp;
        heathBar.value = Hp;
        currentHp = Hp;
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerMove = GetComponent<Player_Move>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        normal = sprite.material;
        layerOrder = sprite.sortingOrder;
    }
    private void Update()
    {
        heathBar.value = currentHp;
    }
    public void TakeDamage(float damage)
    {
        if (invincible || died)
            return;
        currentHp -= damage;
        StartCoroutine(Flash());
        CameraShake.Instance.ShakeCamera(shakeCamForce, shakeCamDuration);
        AudioManager.Instance.PlaySfx(AudioManager.Instance.playerHurt);
        StopTime.Instance.TimeStop(hurtDuration);
    }
    IEnumerator Flash()
    {
        sprite.material = flash;
        sprite.sortingOrder = 9;
        yield return new WaitForSeconds(hurtDuration);
        sprite.material = normal;
        sprite.sortingOrder = layerOrder;
        if (currentHp <= 0)
            StartCoroutine(Death());
        else
            StartCoroutine(Invincibility());
    }
    IEnumerator Invincibility()
    {
        invincible = true;
        Color32 fadeColor = sprite.color;
        fadeColor.a = 180;
        sprite.color = fadeColor;
        yield return new WaitForSeconds(invincibilityTime);
        fadeColor.a = 255;
        sprite.color = fadeColor;
        invincible = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heart"))
        {
            currentHp += collision.GetComponent<Heart>().heal;
            if (currentHp > heathBar.maxValue)
                currentHp = heathBar.maxValue;
            AudioManager.Instance.PlaySfx(AudioManager.Instance.heal);
            Destroy(collision.gameObject);
        }
    }
    IEnumerator Death()
    {
        died = true;
        anim.SetTrigger("die");
        playerMove.enabled = false;
        for(int i = 0; i < inventory.Weapons.Count; i++)
            inventory.Weapons[i].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        GameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
