using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Thorn : MonoBehaviour
{
    public GameObject hitEffect;
    GameObject effect;
    public Stats stats;
    public Coroutine cooldown;
    public ThornLauncher gun;


    void OnCollisionEnter2D(Collision2D collision)
    {
        Stats collisionStats = collision.gameObject.GetComponent<Stats>();
        if (collisionStats != null)
        {
            gun.StopCoroutine(cooldown);
            effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2.0f);
            gameObject.SetActive(false);
            collisionStats.CurrentHealth -= stats.Damage;
        }
    }


}
