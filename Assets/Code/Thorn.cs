using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
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
