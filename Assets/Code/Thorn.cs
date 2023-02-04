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
    GameObject effect;




    void OnCollisionEnter2D(Collision2D collision)
    {

        effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
        gameObject.SetActive(false);
    }


}
