using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI bulletSpeedText;
    public TextMeshProUGUI bulletSizeText;
    bool shown;


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

    private List<GameObject> GetUnactiveBullets()
    {
        return pooledBullets.FindAll(e => !e.activeInHierarchy);
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
            Quaternion rotation = firePoint.rotation;
            rotation *= Quaternion.Euler(0, 0, -180);
            unactiveBullet.transform.SetPositionAndRotation(firePoint.position, rotation);
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

    public void AttackSpeedIncrease()
    {
        int wave = waveManager.wave;

        shootCooldownLength -= 0.015f * (waveManager.wave / 2);

        if (shootCooldownLength < 0 && shown == false)
        {
            shootCooldownLength = 0;
            displayText("Attack Speed Max!", attackSpeedText);
            shown = true;
        }
        else if (shootCooldownLength > 0 && shown == false)
        {
            displayText("Attack Speed Up!", attackSpeedText);
        }
    }

    public void IncreaseBulletSpeed()
    {
        int wave = waveManager.wave;
        float increment = 20f;

        if (wave % 10 == 0 && wave != 0)
        {
            bulletForce += increment;
            displayText("Bullet Speed Up!", bulletSpeedText);
        }
    }

    public void IncreaseBulletSize()
    {
        int wave = waveManager.wave;
        Vector3 increment = new Vector3(1f, 1f, 0.0f);
        List<GameObject> unactiveBullet = GetUnactiveBullets();
        if (wave % 11 == 0 && wave != 0)
        {
            GameObject randomBullet = unactiveBullet[Random.Range(0, unactiveBullet.Count)];
            Vector3 newSize = randomBullet.transform.localScale;
            newSize += increment;
            randomBullet.transform.localScale = newSize;
            displayText("Random Bullet Size Up!", bulletSizeText);
        }
    }

    private void displayText(string message, TextMeshProUGUI textType)
    {
        // Get reference to the TextMeshPro component
        textType = GetComponentInChildren<TextMeshProUGUI>();
        textType.text = message;

        // Start coroutine to display the text for 3 seconds
        StartCoroutine(DisplayTextForDuration(textType, 3f));
    }

    private IEnumerator DisplayTextForDuration(TextMeshProUGUI textMeshPro, float duration)
    {
        // Enable the TextMeshPro component to display the text
        textMeshPro.enabled = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Disable the TextMeshPro component to hide the text
        textMeshPro.enabled = false;
    }
}