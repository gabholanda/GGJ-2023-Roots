using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Stats stats;
    public float distanceLimit;

    private float distance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = GetComponent<Stats>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x);

        if (distance > distanceLimit)
        {
            transform.SetPositionAndRotation(Vector2.MoveTowards(this.transform.position, player.transform.position, stats.Speed
                * Time.deltaTime), Quaternion.Euler(Vector3.forward * angle));
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("i am hitting you");
    }
}
