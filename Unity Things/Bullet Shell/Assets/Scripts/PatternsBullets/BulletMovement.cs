using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - Sets 

 */



public class BulletMovement : MonoBehaviour
{

    private float theta;
    private float deltaTheta;
    private float radius;
    private float customEpsilon = 0.1f; //A very small float. Used for incrementing.
    
    //internal, so that the SpawnBullets method in PatternController can access these.
    internal int bulletID; //based on the spawn order of the bullets in the pattern.
    internal float size; //scale of the pattern
    internal float speed; //speed multiplier for the bullet's movement.

    //variables unique to the Rose graph type
    internal float petals = 4; //number of "petals" seen on the graph.
    internal float graphPeriod; //The value of theta at which point the function starts to trace over itself.
    internal float rows = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        if ( petals/2 == Mathf.Ceil(petals/2)) //checks if even or odd number of petals
        { //if even...
            graphPeriod = 2.0f * Mathf.PI; //the graph's period is 2*pi
        }
        else
        { //if odd...
            graphPeriod = Mathf.PI; // the graph's period is pi.
        }
        theta = bulletID * graphPeriod / petals / rows; //initial value of theta
        transform.position = (new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //Increments theta.
        deltaTheta = customEpsilon * speed * Time.deltaTime * Mathf.PI; //NOTE FOR LATER: update deltaTheta here by setting it to ApproximateDeltaTheta();
        theta += deltaTheta; //NOTE FOR LATER: change theta's value by deltaTheta

        //Calculates the radius as a mathematical function of theta.
        radius = PatternStep(theta) * size;

        //Convert the polar coordinates to rectangular coordinates
        float dx = PolarToX(radius, theta) - transform.localPosition.x;
        float dz = PolarToZ(radius, theta) - transform.localPosition.z;

        //Update the bullet's position.
        transform.Translate(new Vector3(dx, 0, dz));

    }

    float PatternStep(float angle)
    {
        float rad = Mathf.Sin(petals/(graphPeriod/Mathf.PI) * angle); //dividing by (graphPeriod/Mathf.PI) makes the graph have the right number of petals.
        return rad;
    }
    
    float PolarToX(float rad, float angle)
    {
        float x = rad * Mathf.Cos(angle);
        return x;
    }
    
    float PolarToZ(float rad, float angle)
    {
        float z = rad * Mathf.Sin(angle);
        return z;
    }

    float ApproximateDeltaTheta(float distance, float angle)
    {
        //NOTE FOR LATER: add in the Newton's Method approx. of deltaTheta here.
        return angle;
    }
}
