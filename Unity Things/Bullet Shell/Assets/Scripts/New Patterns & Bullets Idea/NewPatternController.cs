using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    NOTE TO SELF: WHAT THIS SCRIPT DOES

    1. Spawns in the bullets (in Start() function)
        a. instantiates the bullets one at a time.
    2. Moves the pattern forward.

 */



public class NewPatternController : MonoBehaviour
{
    private float travelSpeed; //the speed at which the pattern moves across the screen
    private int patternIndex; //the graph/shape of the pattern. 
    public gameObject[] bulletPrefabs; //list of the different bullet prefabs. Each prefab follows a unique pattern.
    private int totalBullets = 10;


    // Start is called before the first frame update
    void Start()
    {
        patternIndex = Random.Range(0, bulletPrefabs.Length);
        travelSpeed = Random.Range(0.5f, 10.0f);

        SpawnBullets(totalBullets, bulletPrefabs[patternIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }

    //spawns a bullet inside the pattern
    void SpawnBullets(int total, gameObject bulletType)
    {
        int bulletCount = 0;
        
        //NOTE: begin loop here (bulletCount <= total)

            //spawns a bullet of the specified type
            gameObject newBullet = Instantiate(bulletType, transform);

            //sets up the bullet's properties
            bulletMovementScript = newBullet.GetComponent<BulletMovement>();

            bulletCount++;
            bulletMovementScript.bulletID = bulletCount;

        //NOTE: end loop here

    }
    
    
}
