using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    NOTE TO SELF: WHAT THIS SCRIPT DOES

    1. Spawns in the bullets (in Start() function)
        a. instantiates the bullets one at a time.
    2. Moves the pattern forward.

 */



public class PatternController : MonoBehaviour
{
    //variables/properties used by the pattern
    private float travelSpeed; //the speed at which the pattern moves across the screen
    private int patternIndex; //the graph/shape of the pattern. 
    public GameObject[] bulletPrefabs; //list of the different bullet prefabs. Each prefab follows a unique pattern.
    private int totalBullets = 1; //Total number of bullets to spawn in the pattern.

    //variables/properties to be passed onto the bullets
    private float bulletSpeed; //the speed at which the bullets move along the path of their pattern.
    private float patternSize; //the scale of the pattern


    // Start is called before the first frame update
    void Start()
    {
        //NOTE FOR LATER: the following two lines can be moved into the SpawnPattern script for the sake of organization.
        patternIndex = Random.Range(0, bulletPrefabs.Length);
        travelSpeed = 0 * Random.Range(0.5f, 10.0f);

        patternSize = 1.0f; //For a noticeable effect, set this to a multiple of 10.
        bulletSpeed = 1.0f; 

        SpawnBullets(totalBullets, bulletPrefabs[patternIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }

    //spawns a bullet inside the pattern
    void SpawnBullets(int total, GameObject bulletType)
    {
        int bulletCount = 0;
        
        //NOTE: begin loop here (bulletCount <= total)

            //spawns a bullet of the specified type
            GameObject newBullet = Instantiate(bulletType, transform);
            bulletCount++;

            //sets up the bullet's properties
            BulletMovement bulletMovementScript = newBullet.GetComponent<BulletMovement>();
            bulletMovementScript.size = patternSize * 10.0f; //for now, just a constant. Later, I might add in some variance between instances of patterns.
            bulletMovementScript.speed = bulletSpeed * 2.0f / patternSize; //for now, just a constant inversely related to size. Later, I might add in some variance between instances of patterns.
            bulletMovementScript.bulletID = bulletCount;

        //NOTE: end loop here

    }
    
    
}
