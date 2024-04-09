using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    public float hp;
    private float currentHp;
    public Slider heathBar;

    public float invincibilityTime;

    public float shakeCamForce;
    public float shakeCamDuration;
    public float timeStop;

    public Material flashMaterial;
    private Material defaultMaterial;

    public GameObject GameOverScreen;


    private bool invincible;
    private bool died;
    private int layerOrder;

    private SpriteRenderer sprite;
    private Animator anim;
    private Player_Move playerMove;
    private Inventory inventory;
    
    private void Awake()
    {
        heathBar.maxValue = hp;
        heathBar.value = hp;
        currentHp = hp;

        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerMove = GetComponent<Player_Move>();

        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        defaultMaterial = sprite.material;
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
        StopTime.Instance.TimeStop(timeStop);
    }

    IEnumerator Flash()
    {
        sprite.material = flashMaterial;
        sprite.sortingOrder = 9;

        yield return new WaitForSeconds(timeStop);

        sprite.material = defaultMaterial;
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

        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
