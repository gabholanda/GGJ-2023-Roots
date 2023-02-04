using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Put unactive prefabs")]
    [SerializeField]
    private List<GameObject> enemyPrefabs;
    private List<GameObject> pooledEnemies = new List<GameObject>();
    [Tooltip("Pool each enemy in enemyPrefabs variable by this quantity")]
    [SerializeField]
    private int pooledQuantity;
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float timeBetweenSpawns;
    [SerializeField]
    private Vector2 spawnMinLocation;
    [SerializeField]
    private Vector2 spawnMaxLocation;

    private void Awake()
    {
        PoolEnemies();
        StartCoroutine(StartSpawning());
    }

    public void SetTimeBetweenSpawns(float time)
    {
        timeBetweenSpawns = time;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    private void PoolEnemies()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < pooledQuantity; j++)
            {
                pooledEnemies.Add(Instantiate(enemyPrefabs[i], gameObject.transform));
            }
        }
    }

    private List<GameObject> GetUnactiveEnemies()
    {
        return pooledEnemies.FindAll(e => !e.activeInHierarchy);
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);

            List<GameObject> unactiveEnemies = GetUnactiveEnemies();
            if (unactiveEnemies.Count > 0)
            {
                GameObject enemyToBeSpawned = unactiveEnemies[Random.Range(0, unactiveEnemies.Count)];
                enemyToBeSpawned.transform.position = GetRandomPosition();
                enemyToBeSpawned.SetActive(true);
            }
        }
    }

    public Vector3 GetRandomPosition()
    {
        int randomCoinFlip = Random.Range(0, 4);

        return randomCoinFlip switch
        {
            //left
            0 => new Vector3(Random.Range(spawnMinLocation.x, spawnMinLocation.x - 2),
                                Random.Range(spawnMinLocation.y, spawnMaxLocation.y)),
            //right
            1 => new Vector3(Random.Range(spawnMaxLocation.x, spawnMaxLocation.x + 2),
                                Random.Range(spawnMinLocation.y, spawnMaxLocation.y)),
            //up
            2 => new Vector3(Random.Range(spawnMinLocation.x, spawnMaxLocation.x),
                                Random.Range(spawnMaxLocation.y, spawnMaxLocation.y + 2)),
            //down
            3 => new Vector3(Random.Range(spawnMinLocation.x, spawnMaxLocation.x),
                                Random.Range(spawnMinLocation.y, spawnMinLocation.y - 2)),
            _ => new Vector3(),
        };
    }
}
