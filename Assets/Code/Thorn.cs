using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
 
    public GameObject hitEffect;
    GameObject effect;
    public Coroutine cooldown;
    public CharacterController gun;



    void OnCollisionEnter2D(Collision2D collision)
    {
        gun.StopCoroutine(cooldown);
        effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
        gameObject.SetActive(false);
        
    }


}
