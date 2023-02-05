using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Stats))]
public class EnemyBrain : MonoBehaviour
{
    public GameObject player;
    public Stats stats;
    public float distanceLimit;
    private float distance;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    public float nextTimeAttack;
    public AudioClip music;
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            attackPoint = player.transform;
        stats = GetComponent<Stats>();
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();


        }
    }


    void Attack()
    {
        if (Time.time >= nextTimeAttack)
        {
            nextTimeAttack = Time.time + attackRate;
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            foreach (Collider2D player in hitPlayers)
            {
                if (player.gameObject.CompareTag("Player"))
                {
                    player.GetComponent<Stats>().CurrentHealth -= stats.Damage;
                    audioSource.PlayOneShot(music);
              
                    break;
           
                }
            }
        }
    }
    void Update()

    {
        if (player)
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
            else
            {
                Attack();
                
            }
        }
    }
}
