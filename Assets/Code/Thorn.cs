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

    // i did what gabriel asked and made it so that we arent constantly creating and destroying hit effects
    // but there are 3 issues
    // issue 1:
    // the character is rotating again idk why yuliya you fixed it you can do it again :D   
    // issue 2:
    // the bullets no longer rotate to be in the direction of the cursor as they are created all at once
    // and i cant figure out how to make them change before i reactivate them again
    // im not even sure if gabriel wanted me to do the polling for the bullets too or just for the hit effect itself so i've left all the old code
    // and my new code is seperated from the original in the CharacterController script, thought im not sure it will work if you use the old code
    // issue 3:
    // if the bullet despawns BEFORE the hiteffect despawns, the hiteffect will last forever and will no longer be able to be deactivated..
    // the issue with this is that im not sure how to make it so that it can be independent of the bullet.
    // if you don't despawn the bullet before the hiteffect then it just kinda flies off for a bit and then despawns off screen
    // if im a bitch to wake up tomorrow because its 8am when im writing this, i wrote all these issues to either fix it yourself or communicate to gabriel.
    // goodluck ;D - Brett P.S added some comments to tell you whats going on a bit
    public GameObject hitEffect;
    GameObject effectToBeDeactivated;
    public List<GameObject> hitPrefabs;
    private List<GameObject> pooledEffects = new List<GameObject>();
    [SerializeField]
    private int pooledQuantity;
    List<GameObject> activeEffects;


    private bool isFirstIteration = true;

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (isFirstIteration) // makes it so the spawn coroutine only runs once
        {
            StartCoroutine(StartSpawning(collision)); // reactivates a hit effect
            isFirstIteration = false;
            gameObject.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) // can run coroutine again
    {
        isFirstIteration = true;
    }
    void Awake()
    {
        hitPrefabs = new List<GameObject>() { hitEffect };
        PollThornsEffect(); // polls the thorns (creates them and deactivates them so they're invisible)

        //Destroy(gameObject, 6.0f);
    }

    private void PollThornsEffect()
    {
        for (int i = 0; i < hitPrefabs.Count; i++)
        {
            for (int j = 0; j < pooledQuantity; j++)
            {
                pooledEffects.Add(Instantiate(hitPrefabs[i], transform.position, Quaternion.identity));
                activeEffects = GetActiveEffects();
                effectToBeDeactivated = activeEffects[i];
                effectToBeDeactivated.SetActive(false);
            }
        }
    }

    private List<GameObject> GetActiveEffects()
    {
        return pooledEffects.FindAll(e => e.activeInHierarchy);
    }
    private List<GameObject> GetUnactiveEffects()
    {
        return pooledEffects.FindAll(e => !e.activeInHierarchy);
    }


    IEnumerator StartSpawning(Collision2D collision) // reactivates the hit effects at the collision location
    {
        while (true)
        {

            List<GameObject> unactiveEffects = GetUnactiveEffects();
            List<GameObject> activeEffects = GetActiveEffects();
            if (unactiveEffects.Count > 0)
            {
                Vector2 spawnPosition = collision.GetContact(0).point;
                GameObject effectToBeActivated = unactiveEffects[Random.Range(0, unactiveEffects.Count)];
                effectToBeActivated.transform.position = spawnPosition;

                effectToBeActivated.SetActive(true);
                activeEffects.Add(effectToBeActivated);
                StartCoroutine(DeactivateEffect(effectToBeActivated));
            }
            yield break;
        }
    }

    IEnumerator DeactivateEffect(GameObject hitEffect) // deactivates the hit effects after 2 seconds
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects = GetActiveEffects();
            yield return new WaitForSeconds(2.0f);
            effectToBeDeactivated = activeEffects[i];
            effectToBeDeactivated.SetActive(false);
        }
    }

}