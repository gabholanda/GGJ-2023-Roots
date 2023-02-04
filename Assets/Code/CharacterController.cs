using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public InputReader reader;
    // This will break if you don't add the five transforms(Pots)
    public Transform[] teleportPoints;
    public Stats stats;

    [SerializeField]
    [Min(0.0f)]
    private float cooldownTime;

    private bool isCoolingDown;

    [Header("Shooting Variables")]
    // my code to make polling for the bullets (refer to issue 2 in thorn if confused
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20.0f;
    private List<GameObject> pooledBullets = new List<GameObject>();
    [SerializeField]
    private int pooledQuantity;
    List<GameObject> activeBullets;



    //TODO: Pass stats damage to bulletPrefab
    //A bullet needs to be independent from the character, as such, its speed and damage will be
    //in a different script (Thorn) and not in the CharacterController script
    private void Awake()
    {
        stats = GetComponent<Stats>();
        PoolBullets();
    }

    private void OnEnable()
    {
        reader.OnFire.performed += OnFire;
        reader.OnFire.Enable();
        reader.OnTeleport.performed += OnTeleport;
        reader.OnTeleport.Enable();
    }

    private void OnDisable()
    {
        reader.OnFire.performed -= OnFire;
        reader.OnFire.Disable();
        reader.OnTeleport.performed -= OnTeleport;
        reader.OnTeleport.Disable();
    }

    void OnFire(InputAction.CallbackContext obj)
    {
        Shoot();
    }

    void OnTeleport(InputAction.CallbackContext callbackContext)
    {
        if (!isCoolingDown)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                transform.position = teleportPoints[0].position;
            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                transform.position = teleportPoints[1].position;
            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                transform.position = teleportPoints[2].position;
            }
            else if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                transform.position = teleportPoints[3].position;
            }
            else if (Keyboard.current.digit5Key.wasPressedThisFrame)
            {
                transform.position = teleportPoints[4].position;
            }

            isCoolingDown = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCoolingDown = false;
    }


    // more code for the bullet pool, again issue 2.
    private void PoolBullets()
    {
        for (int i = 0; i < pooledQuantity; i++)
        {
            pooledBullets.Add(Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
            // Unnecessary chunk of code consuming extra space for creating more variables
            // And repeating a search
            //activeBullets = GetActiveBullets();
            //GameObject bulletToBeDeactivated = activeBullets[i];
            //bulletToBeDeactivated.SetActive(false);
        }

        activeBullets = GetActiveBullets();
        for (int i = 0; i < activeBullets.Count; i++)
        {
            activeBullets[i].SetActive(false);
        }
    }

    private List<GameObject> GetActiveBullets()
    {
        return pooledBullets.FindAll(e => e.activeInHierarchy);
    }

    private GameObject GetUnactiveBullet()
    {
        return pooledBullets.Find(e => !e.activeInHierarchy);
    }

    public void Shoot()
    {
        // Unlike the enemies which we need to randomize which one to spawn,
        // Bullets are all the same and we just need the "next available" one
        GameObject unactiveBullet = GetUnactiveBullet();
        if (unactiveBullet)
        {
            // We set everything we need before activating and shooting
            Rigidbody2D rb = unactiveBullet.GetComponent<Rigidbody2D>();
            unactiveBullet.transform.position = firePoint.position;
            unactiveBullet.SetActive(true);
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            StartCoroutine(DeactivateBullet(unactiveBullet));
        }
    }


    public IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2.0f);
        bullet.SetActive(false);
    }


    // shooting code used before, not sure if it works with the new hiteffect code test it or smn idk
    //void Shoot()
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    //    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    //}

}
