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
//references
    private GameObject gameManager;
    private GameManager gameManagerScript;

//declare vars
    internal int patternType; //This represents whether the pattern's equation is rectangular (1) or polar (2).
    internal int graphIndex; //This represents which graph/paths to use.
    internal float travelSpeed; //The speed at which the whole pattern moves
    internal float bulletSpeed; //The speed at which the bullets move within the pattern
    internal float patternSize; //the scale of the pattern

    internal int bulletsPerRow; //number of bullets per row of the pattern
    internal int rowCount; //number of rows in the pattern (only seems to truly work that way on the Sine pattern, but I actually like how it works with the others.)
    internal float xBound; //the x boundaries (local space) for rectangular patterns
    internal float zBound; //the z boundaries (local space) for rectangular patterns

    public GameObject[] bulletPrefabRectList; //list of bullet prefabs for rectangular patterns
    public GameObject[] bulletPrefabPolList; //list of bullet prefabs for polar patterns
    public GameObject bulletPrefab; //this will be the final choice for the bullet prefab used by this pattern.

    private float startThetaModifier; //this is the starting value of theta, but without any bulletID factored in. It will act as a sort of formula for initializing the values of theta in each bullet as they spawn.
    private float startXPosModifier; //the formula for generating each bullet's initial x position (local space) 
    private float startZPosModifier; //the formula for generating each bullet's initial z position (local space) 



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        float gameTime = gameManagerScript.playTime;
        //sets up all of the variables of the pattern

        if (gameTime < 20) 
        {
            graphIndex = 0;
            patternType = 2;
        }
        else if (gameTime < 40 ) 
        {
            graphIndex = Random.Range(0, 2);
            patternType = 2;
        }
        else if (gameTime < 60 ) 
        {
            patternType = Random.Range(1,3);
            graphIndex = Random.Range(0, 2);
            if (patternType == 1) //If the pattern is rectangular...
            {
                graphIndex = 0; //... Spawn only walls
            }
        }
        else 
        {
            graphIndex = Random.Range(0, 2);
            patternType = Random.Range(1, 3);
        }
        // graphIndex = Random.Range(0, 2);//1;//
        patternSize = 10f;
        if ( gameManagerScript.playTime < 100)
        { 
            travelSpeed = 5.0f + .05f * gameManagerScript.playTime;//Random.Range(1.0f, 20.0f);
        }
        else
        {
            travelSpeed = 10f;
        }
        if (patternType == 1)
        {
            switch (graphIndex)
            {


                case 0: //wall | Third type
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
                case 1: //snake | Fourth type
                    bulletsPerRow = 51;
                    rowCount = 1;
                    xBound = 5.0f;
                    zBound = 7.0f;
                    startXPosModifier = xBound / bulletsPerRow;
                    startZPosModifier = 0;
                    bulletSpeed = 30.0f;
                    travelSpeed *= 2.0f;
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
                case 0: //Rose20 | First type
                    bulletsPerRow = 20; //set up how many bullets belong in each row.
                    rowCount = 2; //set up how many rows belong in the pattern.
                    startThetaModifier = 2.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    bulletSpeed = 0.1f;
                    patternSize = 16.0f;
                    break;
                case 1: //Cross | Second Type
                    bulletsPerRow = 10; //set up how many bullets belong in each row.
                    rowCount = 1; //set up how many rows belong in the pattern.
                    bulletSpeed = 2.0f;
                    startThetaModifier = 5.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                    patternSize = 7.0f;
                    break;
                // case 2: //Rose8
                //     bulletsPerRow = 8; //set up how many bullets belong in each row.
                //     rowCount = 2; //set up how many rows belong in the pattern.
                //     startThetaModifier = 2.0f * Mathf.PI / bulletsPerRow / rowCount; //create the formula for initializing the theta values for bullets.
                //     bulletSpeed = 0.5f;
                //     break;
                default:
                    Debug.Log("error: Graph type not defined");
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
        bool isGameOver = GameObject.Find("Game Manager").GetComponent<GameManager>().isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
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
