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
    public ThornLauncher launcher;
    [SerializeField]
    [Min(0.0f)]
    private float cooldownTime;
    private bool isCoolingDown;



    private void Awake()
    {
        stats = GetComponent<Stats>();


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
        launcher.Shoot();
        launcher.ShootEffect();
   
    }

    void OnTeleport(InputAction.CallbackContext callbackContext)
    {
        if (!isCoolingDown)
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Vector3 newPosition = teleportPoints[0].position;
                newPosition.y += 0.64f;
                transform.position = newPosition;

            }
            else if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                Vector3 newPosition = teleportPoints[1].position;
                newPosition.y += 0.64f;
                transform.position = newPosition;

            }
            else if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                Vector3 newPosition = teleportPoints[2].position;
                newPosition.y += 0.64f;
                transform.position = newPosition;

            }
            else if (Keyboard.current.digit4Key.wasPressedThisFrame)
            {
                Vector3 newPosition = teleportPoints[3].position;
                newPosition.y += 0.64f;
                transform.position = newPosition;

            }
            else if (Keyboard.current.digit5Key.wasPressedThisFrame)
            {
                Vector3 newPosition = teleportPoints[4].position;
                newPosition.y += 0.64f;
                transform.position = newPosition;

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


}
