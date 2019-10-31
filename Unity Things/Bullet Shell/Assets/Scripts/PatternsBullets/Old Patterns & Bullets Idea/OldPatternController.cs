using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    NOTE TO SELF: WHAT THIS SCRIPT DOES

    1. Sets up the pattern data on spawn (in Start() function)
        a. Chooses patternType (1 for rectangular or 2 for polar)
        b. Chooses graphIndex (a value to be used for retrieving the equation from a set of them)
        c. Chooses travelSpeed (how fast the pattern overall moves)
        d. Chooses bulletSpeed (how fast the bullets move within the pattern)
    2. Spawns in the bullets (in Start() function)
        a. instantiates the bullets one at a time.
        b. initializes their values of theta.

 */

public class OldPatternController : MonoBehaviour
{
    //declare vars
        public int patternType; //This represents whether the pattern's equation is rectangular (1) or polar (2).
        public int graphIndex; //This represents which graph/paths to use.
        public float travelSpeed; //The speed at which the whole pattern moves
        public float bulletSpeed; //The speed at which the bullets move within the pattern
        public float patternSize; //the scale of the pattern

        public int bulletsPerRow;
        public int rowCount;

        public GameObject[] bulletPrefabList;

        public bool full = false;
        public bool nextSpawnReady = true;
        public float startThetaModifier; //this is the starting value of theta, but without any bulletID facotred in. It will act as a sort of formula for initializing the values of theta in each bullet as they spawn.

    // Start is called before the first frame update
    void Start()
    {
        //sets up all of the variables of the pattern
        patternType = 2;//Random.Range(1, 3);
        graphIndex = Random.Range(0, 2); //1;//
        travelSpeed = 5.0f;//Random.Range(1.0f, 20.0f);
        bulletSpeed = 1.0f;//Random.Range(1.0f, 1.5f);
        patternSize = 1.0f;
        // bulletsPerRow = Random.Range(1, 16);


        if (patternType == 1)
        {
            switch (graphIndex)
            {
                case 0:
                    //empty for now
                    break;
                case 1:
                    //empty for now
                    break;
            }
            //empty for now
        }
        else if (patternType == 2)
        {
            switch (graphIndex)
            {
                case 0: //Rose
                    bulletsPerRow = 4; //set up how many bullets belong in each row.
                    rowCount = 2; //set up how many rows belong in the pattern.
                    startThetaModifier = 2.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    break;
                case 1: //Cross
                    bulletsPerRow = 5; //set up how many bullets belong in each row.
                    rowCount = 2; //set up how many rows belong in the pattern.
                    startThetaModifier = 5.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }


        //REMEMBER TO ADD: loop this according to totalCount, and track the number of bullets spawned with bulletCount inside the loop.
        int bulletCount = 0;
        while ( (bulletCount < bulletsPerRow*rowCount && bulletCount < 30))
        {
            bulletCount++;
            spawnBullet(bulletCount);
            nextSpawnReady = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //moves the pattern forward
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }
    
    void spawnBullet(int currentBullet)
    {
        GameObject newBullet = Instantiate(bulletPrefabList[graphIndex], transform);
        OldBulletMovement bulletMovementScript = newBullet.GetComponent<OldBulletMovement>();
        
        bulletMovementScript.bulletID = currentBullet;

        bulletMovementScript.patternType = patternType;
        bulletMovementScript.graphIndex = graphIndex;
        bulletMovementScript.directionModifier = ((Random.Range(1, 3) - 1) * 2) - 1;
        bulletMovementScript.size = patternSize * 10.0f;
        bulletMovementScript.speed = bulletSpeed * 2 / patternSize;
        bulletMovementScript.bulletsPerRow = bulletsPerRow;
        bulletMovementScript.rowCount = rowCount;
        bulletMovementScript.theta = startThetaModifier * currentBullet;


        // currentBullet++;
        // if (!full)
        // {
        //     spawnBullet(currentBullet);
        // }
    }
}
