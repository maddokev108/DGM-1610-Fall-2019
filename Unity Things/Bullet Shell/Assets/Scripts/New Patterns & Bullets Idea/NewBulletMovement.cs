using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - Sets 

 */



public class NewBulletMovement : MonoBehaviour
{
    private float theta;
    private float deltaTheta;
    private float radius;
    private float speed = 1.0f; //speed multiplier for the bullet's movement. 
    private float customEpsilon = .0001f;
    private int bulletID;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        theta = 0; //NOTE FOR LATER: initialize theta based on the bulletID, such that the bullets are evenly distributed across the pattern.
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the next point on the graph
        deltaTheta = customEpsilon * speed * Time.deltaTime; //NOTE FOR LATER: update deltaTheta here by setting it to ApproximateDeltaTheta();
        theta = theta + deltaTheta; //NOTE FOR LATER: change theta's value by deltaTheta
        radius = PatternStep(theta);

        //Convert the polar coordinates to rectangular coordinates
        float dx = Mathf.abs( PolarToX(radius, theta) - transform.position.x );
        float dy = Mathf.abs( PolarToZ(radius, theta) - transform.position.z );

        //Update the bullet's position.
        transform.Translate(new Vector3(xPos, 0, zPos));
    }

    float PatternStep(float angle)
    {
        float rad = Mathf.Sin(2*angle);
        return rad;
    }
    
    float PolarToX(float rad, float angle){
        float x = rad * Mathf.Sin(angle);
        return x;
    }
    
    float PolarToZ(float rad, float angle){
        float z = rad * Mathf.Cos(angle);
    }

    float ApproximateDeltaTheta(float distance, float angle)
    {
        float angle;//NOTE FOR LATER: add in the Newton's Method approx. of deltaTheta here.
        return angle;
    }
}
