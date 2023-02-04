using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    //TODO: It is preferable to poll GameObjects instead of keep instantiating/destroying them all the time
    //because of performance issues
    //See EnemySpawner for reference
    //The idea is to instantiate a certain quantity in the Awake() and then grab only active == false
    //from the list
    //How exactly are you gonna do that? idk
    public GameObject hitEffect;
    public GameObject effect;
    public float collide = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {

        effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.0f);
        Destroy(gameObject);



    }
    void Start()
    {
        Destroy(gameObject, 6.0f);
    }

}
