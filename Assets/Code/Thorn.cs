using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
<<<<<<< Updated upstream
 
    public GameObject hitEffect;
    GameObject effect;
    public Coroutine cooldown;
    public CharacterController gun;
=======

    public GameObject hitEffect;
    GameObject effect;
    public Coroutine cooldown;
    public ThornLauncher gun;
>>>>>>> Stashed changes



    void OnCollisionEnter2D(Collision2D collision)
    {
        gun.StopCoroutine(cooldown);
        effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
        gameObject.SetActive(false);
<<<<<<< Updated upstream
        
=======

>>>>>>> Stashed changes
    }


}
