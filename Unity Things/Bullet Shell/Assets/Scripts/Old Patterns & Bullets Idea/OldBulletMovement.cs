using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOTE TO SELF: WHAT THIS SCRIPT DOES
      - Updates the Bullet's position along the pattern's path.
             1. Gets the parent's patternType to see if it's polar or rectangular.
             2. Gets the parent's graphIndex to see what calculation to use.
             
            IF RECTANGULAR:
             3. Follows 1 of these 2 processes (uses a switch statement):
                    Equation-Based:
                      4. increments the value of X.
                      5. calculates the value of z as a function of x.
                    
                    X and Z are Independent:
                      4. changes X
                      5. changes Z
                EXITS THE SWITCH
                EXITS THE IF-ELSE-IF
            IF POLAR:
                In a Switch Statement, where each case stores some different graph shape:
                 3. Increments the value of theta.
                 4. Calculates radius as a function of theta
                EXITS THE SWITCH
             5. Calculates X as radius*cos(theta)
             6. Calculates Z as radius*sin(theta)
            EXITS THE IF-ELSE-IF
             7. Moves the bullet to the new X/Z coordinates.
 */

public class OldBulletMovement : MonoBehaviour
{
    //properties controlling the bullet's behaviour
    public int patternType;
    public int graphIndex;
    public string[] graphRectList;
    public string[] graphPolList;
    public int directionModifier;
    public float speed;
    public float bulletID; //mostly just used for determining the initial x/z position and/or the initial value of theta. 

    public float epsilon = 10.0f; //a very small float.

    //pattern boundaries
    private float xBound = 3;
    private float zBound = 7;    

    //polar variable set
    public float radius;
    public float dr; //delta radius
    public float theta = 0; //for now, just starts at 0. Later on I'll spawn the bullets evenly across the graph by using their bulletID values.
    public float dth; //delta theta
    public float size = 1; //used for scaling the graph.

    //rectangular variable set
    public float xPos = 0; //starting at 0 for now. Later on I'll spawn the bullets evenly across the graph by using their bulletID values.
    public float dx; //delta x
    public float zPos = 0; //starting at 0 for now. Later on I'll spawn the bullets evenly across the graph by using their bulletID values.
    public float dz; //delta z



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (patternType == 1)
        {
            /*temp disable.
            switch(graphIndex)
            {
                case 0:
                    dx = epsilon;
                    dz = epsilon;
                    xPos = xPos + (dx * speed * directionModifier * Time.deltaTime);
                    zPos = zPos + (dz * speed * directionModifier * Time.deltaTime);
                    break;
                case 1:
                    goto case 1;
                default:
                    Debug.Log("error: no case specified for graph type " + graphRectList[graphIndex]);
                    break;
            }
            dx = xPos-transform.position.x;
            dz = zPos-transform.position.z;
            transform.Translate(new Vector3(dx, 0, dz));
            boundCheck();
            */
            patternType = 2;
        }
        else if (patternType == 2)
        {
            switch(graphIndex)
            {
                case 0:
                    dth = epsilon * speed;
                    theta += dth * directionModifier; // * Time.deltaTime;
                    radius = Mathf.Sin(2*theta);
                    break;

                case 1:
                    dth = epsilon * speed;
                    theta += dth * directionModifier; // * Time.deltaTime;
                    radius = Mathf.Cos((3/5)*theta);
                    break;
                    
                default:
                    Debug.Log("error: no case specified for graph type " + graphPolList[graphIndex]);
                    break;
            }
        xPos = radius*Mathf.Cos(theta);//*size;
        zPos = radius*Mathf.Sin(theta);//*size;
        dx = xPos-transform.localPosition.x;
        dz = zPos-transform.localPosition.z;
        transform.Translate(new Vector3(dx, 0, dz));
        }
        // else
        // {
        //     Debug.Log("error: invalid patternType");
        // }

    }
    void boundCheck()
    {
        if (xPos > xBound || xPos < -xBound)
        {
            dx *= -1;
        }
        if (zPos > zBound || zPos < -zBound)
        {
            dz *= -1;
        }
    }
}
