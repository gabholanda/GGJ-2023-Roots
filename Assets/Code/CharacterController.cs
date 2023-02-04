using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public InputReader reader;
    public Transform firePoint;
    public Transform[] teleportPoints;
    public GameObject bulletPrefab;
    public float bulletForce = 20.0f;

    [SerializeField]
    [Min(0.0f)]
    private float cooldownTime;

    private bool isCoolingDown;

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

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

}
