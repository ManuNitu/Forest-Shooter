using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    public int ammo;
    private int currentAmmo;
    public int shootBulletsNumber;
    public float spreadAngle;
    public float shootCooldown;
    private float currentShootCooldown;
    public float reloadSpeed;
    public float camShakeForce;
    public float camShakeDuration;

    public GameObject bullet;
    public Transform shootPoint;
    public bool automatic;
    public bool burst;
    public AudioSource sound;

    private Transform player;
    private Player_Health health;
    private Animator anim;
    private AmmoUI ammoUI;
    private Inventory inventory;
    private bool reloading;
    private bool shooting;
    private void OnEnable()
    {
        anim.Play("Gun_Norm");
        anim.speed = reloadSpeed;
        reloading = false;
    }
    private void Awake()
    {
        ammoUI = GameObject.FindGameObjectWithTag("Ammo").GetComponent<AmmoUI>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = player.GetComponent<Player_Health>();
        anim = GetComponentInParent<Animator>();
        currentAmmo = ammo;
    }
    private void Update()
    {
        inventory.shooting = shooting;
        inventory.reloading = reloading;
        ammoUI.ammo = ammo;
        ammoUI.currentAmmo = currentAmmo;

        if (!reloading)
        {
            if (currentAmmo == 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < ammo))
            {
                reloading = true;
                anim.SetTrigger("reload");
                AudioManager.Instance.PlaySfx(AudioManager.Instance.reload);
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Gun_Norm") && reloading)
            Reload();
    }
    private void LateUpdate()
    {
        if (currentShootCooldown <= 0)
        {
            if (reloading)
                return;
            if ((!automatic && Input.GetMouseButtonDown(0)) || (automatic && Input.GetMouseButton(0)))
            {
                StartCoroutine(Shoot());
                currentShootCooldown = shootCooldown;
            }
        }
        else
        {
            currentShootCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (reloading)
            return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz);
        if (rotz < -90 || rotz > 90)
        {
            if (player.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotz);
            }
            else if (player.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotz);
            }
        }
    }
    IEnumerator Shoot()
    {
        int mid = shootBulletsNumber / 2;
        shooting = true;
        for (int i = 0; i < shootBulletsNumber; i++)
        {
            CameraShake.Instance.ShakeCamera(camShakeForce, camShakeDuration);
            AudioManager.Instance.PlaySfx(sound);
            Vector3 rot = shootPoint.rotation.eulerAngles;
            rot.z += (spreadAngle * mid) - (spreadAngle * i);
            Instantiate(bullet, shootPoint.position, Quaternion.Euler(rot));
            currentAmmo--;
            if (burst)
                yield return new WaitForSeconds(0.1f);
        }
        shooting = false;
       
    }
    public void Reload()
    {
        currentAmmo = ammo;
        reloading = false;
    }

}
