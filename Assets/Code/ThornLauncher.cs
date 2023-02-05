using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornLauncher : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20.0f;
    private List<GameObject> pooledBullets = new List<GameObject>();
    [SerializeField]
    private int pooledQuantity;
    List<GameObject> activeBullets;

    public void Awake()
    {
        PoolBullets();
    }
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
            unactiveBullet.transform.rotation = firePoint.rotation;
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
}
