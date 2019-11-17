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

public class PatternController : MonoBehaviour
{
    //declare vars
        public int patternType; //This represents whether the pattern's equation is rectangular (1) or polar (2).
        public int graphIndex; //This represents which graph/paths to use.
        public float travelSpeed; //The speed at which the whole pattern moves
        public float bulletSpeed; //The speed at which the bullets move within the pattern
        public float patternSize; //the scale of the pattern

        public int bulletsPerRow;
        public int rowCount;
        public float xBound; //the x boundaries (local space) for rectangular patterns
        public float zBound; //the z boundaries (local space) for rectangular patterns

        public GameObject[] bulletPrefabRectList; //list of bullet prefabs for rectangular patterns
        public GameObject[] bulletPrefabPolList; //list of bullet prefabs for polar patterns
        public GameObject bulletPrefab; //this will be the final choice for the bullet prefab used by this pattern.

        public float startThetaModifier; //this is the starting value of theta, but without any bulletID factored in. It will act as a sort of formula for initializing the values of theta in each bullet as they spawn.
        public float startXPosModifier; //the formula for generating each bullet's initial x position (local space) 
        public float startZPosModifier; //the formula for generating each bullet's initial z position (local space) 



    // Start is called before the first frame update
    void Start()
    {
        //sets up all of the variables of the pattern
        patternType = Random.Range(1, 3);//1;//
        graphIndex = Random.Range(0, 2);//1;//
        travelSpeed = 5.0f;//Random.Range(1.0f, 20.0f);
        patternSize = 10f;
        

        if (patternType == 1)
        {
            switch (graphIndex)
            {
                case 0:
                    bulletsPerRow = 21;
                    rowCount = 1;
                    xBound = 50.0f;
                    zBound = 7.0f;
                    startXPosModifier = xBound / bulletsPerRow;
                    startZPosModifier = 0;
                    bulletSpeed = 2.0f;
                    patternSize = 0.1f;
                    // travelSpeed = 2.0f;
                    break;
                case 1:
                    bulletsPerRow = 51;
                    rowCount = 1;
                    xBound = 5.0f;
                    zBound = 7.0f;
                    startXPosModifier = xBound / bulletsPerRow;
                    startZPosModifier = zBound/ rowCount;
                    bulletSpeed = 30.0f;
                    graphIndex = 1;
                    // travelSpeed = 2.0f;
                    break;
                
            }
            //empty for now
                        bulletPrefab = bulletPrefabRectList[graphIndex];

        }
        else if (patternType == 2)
        {
            bulletPrefab = bulletPrefabPolList[graphIndex];
            switch (graphIndex)
            {
                case 0: //Rose20
                    bulletsPerRow = 20; //set up how many bullets belong in each row.
                    rowCount = 2; //set up how many rows belong in the pattern.
                    startThetaModifier = 2.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    bulletSpeed = 0.1f;
                    patternSize = 8.0f;
                    break;
                case 1: //Cross
                    bulletsPerRow = 10; //set up how many bullets belong in each row.
                    rowCount = 1; //set up how many rows belong in the pattern.
                    bulletSpeed = 2.0f;
                    startThetaModifier = 5.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    patternSize = 7.0f;
                    break;
                case 2: //Rose8
                    bulletsPerRow = 8; //set up how many bullets belong in each row.
                    rowCount = 2; //set up how many rows belong in the pattern.
                    startThetaModifier = 2.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    bulletSpeed = 0.5f;
                    break;
                default:
                    Debug.Log("error");
                    break;
            }
        }


        //Spawns bullets into the pattern. 
        int bulletCount = 0;
        while ( (bulletCount < bulletsPerRow * rowCount  &&  bulletCount < 100))
        {
            bulletCount++;
            spawnBullet(bulletCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool gameOver = GameObject.Find("Player").GetComponent<CollisionDetection>().gameOver;
        if (!gameOver) //checks to see if the game is still running.
        {
            //moves the pattern forward
            transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
        }
    }
    
    void spawnBullet(int currentBullet)
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        BulletMovement bulletMovementScript = newBullet.GetComponent<BulletMovement>();

        if (patternType == 1)
        {
            // newBullet = Instantiate(bulletPrefabRectList[graphIndex], transform);
            newBullet.transform.localPosition = new Vector3(startXPosModifier * (bulletsPerRow * rowCount / 2 - currentBullet + 1), 0, startZPosModifier * currentBullet);

        }
        else if (patternType == 2)
        {
            // newBullet = Instantiate(bulletPrefabPolList[graphIndex], transform);
            bulletMovementScript.theta = startThetaModifier * currentBullet;
        }

        
        
        bulletMovementScript.bulletID = currentBullet;

        bulletMovementScript.patternType = patternType;
        bulletMovementScript.graphIndex = graphIndex;

        bulletMovementScript.bulletsPerRow = bulletsPerRow;
        bulletMovementScript.rowCount = rowCount;
        bulletMovementScript.size = patternSize;

        // bulletMovementScript.directionModifier = ((Random.Range(1, 3) - 1) * 2) - 1;
        bulletMovementScript.speed = bulletSpeed*10 / patternSize;


    }
}
