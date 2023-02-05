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
    bool shootCooldown = false;
    public float shootCooldownLength = 0.25f;
    public GameObject shootEffect;
    GameObject effect;
    GameObject unactiveBullet;
    public WaveManager waveManager;

    public void Awake()
    {
        PoolBullets();
    }
    private void PoolBullets()
    {
        for (int i = 0; i < pooledQuantity; i++)
        {
            pooledBullets.Add(Instantiate(bulletPrefab, firePoint.position, firePoint.rotation));
        }

        activeBullets = GetActiveBullets();
        for (int i = 0; i < activeBullets.Count; i++)
        {
            activeBullets[i].SetActive(false);
            activeBullets[i].GetComponent<Thorn>().gun = this;
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
        unactiveBullet = GetUnactiveBullet();
        if (unactiveBullet && shootCooldown == false)
        {
            Rigidbody2D rb = unactiveBullet.GetComponent<Rigidbody2D>();
            unactiveBullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);                                                                                                                                                                                                    
            unactiveBullet.SetActive(true);
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            unactiveBullet.GetComponent<Thorn>().cooldown = StartCoroutine(DeactivateBullet(unactiveBullet));
            shootCooldown = true;
            StartCoroutine(ShootCooldown());
        }
    }


    public IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3.0f);
        if (bullet.activeInHierarchy)
        {
            bullet.SetActive(false);
        }
    }

    public IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldownLength);
        shootCooldown = false;
    }

    public void StopCouroutine(Coroutine couroutine)
    {
        StopCouroutine(couroutine);
    }

    public void playEffect() // not working idk why
    {
        effect = Instantiate(shootEffect, firePoint.position, Quaternion.identity);
        effect.transform.position = firePoint.position;
        Destroy(effect, 2.0f);
    }

    public void attackSpeedIncrease()
    {
        int wave;
        Debug.Log(waveManager.wave);
        wave = waveManager.wave;

        shootCooldownLength -= 0.01f * (waveManager.wave / 2);
        if (shootCooldownLength < 0)
            shootCooldownLength = 0;
    }
}