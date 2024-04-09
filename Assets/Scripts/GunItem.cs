using UnityEngine;
using UnityEngine.UI;

public class GunItem : MonoBehaviour
{
    public GameObject Gun;
    public GameObject UI;

    private GameObject gunHolder;
    private Inventory inventory;

    private void Awake()
    {
        gunHolder = GameObject.FindGameObjectWithTag("GunHolder").gameObject;
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AddGun();
            Destroy(this.gameObject);
        }
    }

    void AddGun()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.reload);

        GameObject gunUI = Instantiate(UI, UI.transform.position, Quaternion.identity, inventory.transform);
        inventory.WeaponsUI.Add(gunUI.GetComponent<Image>());
        
        GameObject theGun = Instantiate(Gun, gunHolder.transform.position, Quaternion.identity, gunHolder.transform);
        inventory.Weapons.Add(theGun.GetComponent<Gun>());

        inventory.ChangeWeapon(inventory.Weapons.Count - 1);
    }
}
