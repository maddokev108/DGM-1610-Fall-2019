using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - instantiates the bullet patterns off-screen
          + rotates the bullet patterns to face some direction such that they travel across the screen.
 */

public class SpawnPatterns : MonoBehaviour
{
    public GameObject patternPrefab;
    public float rot;
    private float xBound = 17.8f;
    private float zBound = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPattern", Random.Range(1.0f, 2.0f));
    }

    //Spawn a bullet pattern
    void SpawnPattern()
    {
        //Chooses random off-screen position
        int rand = (( Random.Range(1, 3) - 1 ) * 2) - 1; //Steps: |1. RNG| 1 or 2. |2. N-1| 0 or 1. |3. N*2| 0 or 2. |4. N-1| -1 or 1.
        float xPos = ( xBound * Random.Range(1.0f, 1.25f) + 7) * rand;
        rand = (( Random.Range(1, 3) - 1 ) * 2) - 1;
        float zPos = ( zBound * Random.Range(1.0f, 1.5f) + 7) * rand;

        //Chooses an angle facing the play area. 
        rot = Mathf.Atan2(zPos, -1*xPos) * 180 / Mathf.PI + 90 + Random.Range(-20.0f, 20.0f); 

        //Instantiates the pattern, then rotates it to face the play area.
        GameObject newPattern = Instantiate(patternPrefab);
        newPattern.GetComponent<Transform>().position = new Vector3(xPos, 0.8f, zPos);
        newPattern.GetComponent<Transform>().Rotate(0, rot, 0);
        

        Invoke("SpawnPattern", Random.Range(1.0f, 5.0f)); //NOTE FOR LATER: put this line inside a conditional to check that the game is still running
    }

}
