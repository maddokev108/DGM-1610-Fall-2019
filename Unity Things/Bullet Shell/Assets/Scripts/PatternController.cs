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
        public int totalCount;
        public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //sets up all of the variables of the pattern
        patternType = Random.Range(1, 3);
        graphIndex = Random.Range(0, 2);
        travelSpeed = Random.Range(1.0f, 20.0f);
        bulletSpeed = Random.Range(1.0f, 1.5f);
        totalCount = Random.Range(1, 16);

        //REMEMBER TO ADD: loop this according to totalCount, and track the number of bullets spawned with bulletCount inside the loop.
        int bulletCount=0;
        spawnBullet(bulletCount);
    }

    // Update is called once per frame
    void Update()
    {
        //moves the pattern forward
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }
    
    void spawnBullet(int currentBullet)
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        newBullet.GetComponent<BulletMovement>().patternType = patternType;
        newBullet.GetComponent<BulletMovement>().graphIndex = graphIndex;
        newBullet.GetComponent<BulletMovement>().speed = bulletSpeed;
        newBullet.GetComponent<BulletMovement>().bulletID = currentBullet;
        newBullet.GetComponent<BulletMovement>().directionModifier = ((Random.Range(1, 3) - 1) * 2) - 1;
    }
}
