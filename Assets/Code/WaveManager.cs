using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner spawner;
    [Min(1)]
    [SerializeField]
    private int initialThreshold = 10;
    [Min(1)]
    private int currentThresold;
    [Min(1)]
    [SerializeField]
    private int incrementPerWave = 15;
    [Min(0.01f)]
    [SerializeField]
    private float spawnDecrement = 0.1f;
    [SerializeField]
    private int currentKilled;
    public ThornLauncher thornLauncher;

    public int wave = 1;

    void Awake()
    {
        currentKilled = 0;
        currentThresold = initialThreshold;
        spawner = GetComponent<EnemySpawner>();
        spawner.PollEnemies();
        for (int i = 0; i < spawner.GetPooledEnemies().Count; i++)
        {
            spawner.GetPooledEnemies()[i].GetComponent<Stats>().OnDeath += CheckCurrentWave;
        }
        StartCoroutine(spawner.StartSpawning());
    }

    void CheckCurrentWave()
    {
        currentKilled++;
        if (currentKilled > currentThresold)
        {
            wave++;
            thornLauncher.AttackSpeedIncrease();
            thornLauncher.IncreaseBulletSpeed();
            thornLauncher.IncreaseBulletSize();
            //thornLauncher.GainBomb();
            currentThresold += incrementPerWave;
            spawner.SetTimeBetweenSpawns(spawner.GetTimeBetweenSpawns() - spawnDecrement);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < spawner.GetPooledEnemies().Count; i++)
        {
            spawner.GetPooledEnemies()[i].GetComponent<Stats>().OnDeath -= CheckCurrentWave;
        }
    }
}
