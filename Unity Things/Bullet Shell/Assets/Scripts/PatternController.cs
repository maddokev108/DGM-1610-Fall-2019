using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    NOTE TO SELF: WHAT THIS SCRIPT DOES

    1. Sets up the pattern on spawn (in Start() function)
        a. Chooses patternType (1 for rectangular or 2 for polar)
        b. Chooses graphIndex (a value to be used for retrieving the equation from a set of them)
        c. Chooses travelSpeed (how fast the pattern overall moves)
        d. Chooses bulletSpeed (how fast the bullets move within the pattern)

 */

public class PatternController : MonoBehaviour
{
    //declare vars
        public int patternType; //This represents whether the pattern's equation is rectangular (1) or polar (2).
        public int graphIndex; //This represents which graph/paths to use.
        public float travelSpeed; //The speed at which the whole pattern moves
        public float bulletSpeed; //The speed at which the bullets move within the pattern

    // Start is called before the first frame update
    void Start()
    {
        //sets up all of the variables of the pattern
        patternType = Random.Range(1, 3);
        graphIndex = Random.Range(0, 5);
        travelSpeed = Random.Range(1.0f, 20.0f);
        bulletSpeed = Random.Range(1.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //moves the pattern forward
        transform.Translate(Vector3.forward * Time.deltaTime * travelSpeed);
    }
}
