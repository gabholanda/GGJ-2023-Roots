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

    public void SetTimeBetweenSpawns(float time)
    {
        if (time < 0)
        {
            timeBetweenSpawns = 0;
        }
        else
        {
            timeBetweenSpawns = time;
        }
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public void PollEnemies()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < pooledQuantity; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i], gameObject.transform);
                enemy.SetActive(false);
                pooledEnemies.Add(enemy);
            }
        }
    }

    private List<GameObject> GetUnactiveEnemies()
    {
        return pooledEnemies.FindAll(e => !e.activeInHierarchy);
    }

    public IEnumerator StartSpawning()
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

    public List<GameObject> GetPooledEnemies()
    {
        return pooledEnemies;
    }
}
