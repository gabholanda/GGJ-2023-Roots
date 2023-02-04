using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public InputReader reader;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20.0f;

    private void OnEnable()
    {
        reader.OnFire.performed += OnFire;
        reader.OnFire.Enable();
    }

    private void OnDisable()
    {
        reader.OnFire.performed -= OnFire;
        reader.OnFire.Disable();
    }

    void OnFire(InputAction.CallbackContext obj)
    {
        Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

}
