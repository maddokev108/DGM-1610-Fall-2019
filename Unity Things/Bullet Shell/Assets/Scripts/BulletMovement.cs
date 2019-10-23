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
    private float customEpsilon = .1f; //A very small float. Used for incrementing.
    
    //internal, so that the SpawnBullets method in PatternController can access these.
    internal int bulletID; //based on the spawn order of the bullets in the pattern.
    internal float size; //scale of the pattern
    internal float speed; //speed multiplier for the bullet's movement.


    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        theta = 0 * Mathf.PI; //NOTE FOR LATER: initialize theta based on the bulletID, such that the bullets are evenly distributed across the pattern.
        transform.position = (new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        //Increments theta.
        deltaTheta = customEpsilon * speed * Time.deltaTime * Mathf.PI; //NOTE FOR LATER: update deltaTheta here by setting it to ApproximateDeltaTheta();
            Debug.Log("deltaTheta: " + deltaTheta);
        theta += deltaTheta; //NOTE FOR LATER: change theta's value by deltaTheta
            Debug.Log("theta: " + theta);

        //Calculates the radius as a mathematical function of theta.
        radius = PatternStep(theta) * size;
            Debug.Log("radius: " + radius);

        //Convert the polar coordinates to rectangular coordinates
        float dx = PolarToX(radius, theta) - transform.localPosition.x;
        float dz = PolarToZ(radius, theta) - transform.localPosition.z;
            Debug.Log("dx: " + dx);
            Debug.Log("dz: " + dz);

        //Update the bullet's position.
        transform.Translate(new Vector3(dx, 0, dz));

    }

    float PatternStep(float angle)
    {
        float rad = Mathf.Sin(2*angle);
        return rad;
    }
    
    float PolarToX(float rad, float angle){
        float x = rad * Mathf.Cos(angle);
        return x;
    }
    
    float PolarToZ(float rad, float angle){
        float z = rad * Mathf.Sin(angle);
        return z;
    }

    float ApproximateDeltaTheta(float distance, float angle)
    {
        //NOTE FOR LATER: add in the Newton's Method approx. of deltaTheta here.
        return angle;
    }
}
