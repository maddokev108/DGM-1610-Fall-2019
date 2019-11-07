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
    //Pattern storage
    public int patternType;
    public int graphIndex;
    public string[] graphRectList;
    public string[] graphPolList;

    //pattern info
    private float xBound = 3; //arbitrary values for now.
    private float zBound = 7; //arbitrary values for now.
    public int rowCount;
    public int bulletsPerRow;

    //properties controlling the bullet's behaviour
    internal int directionModifier=1;
    internal float speed;
    public float bulletID; //mostly just used for determining the initial x/z position and/or the initial value of theta. 

    private float epsilon = 0.10f; //small float used for incrementing.


    //polar variable set
    public float radius;
    public float deltaRadius; //delta radius
    public float theta;
    public float deltaTheta; //delta theta
    public float size; //used for scaling the graph.

    //rectangular variable set
    public float xPos = 0; //starting at 0 for now. Later on I'll spawn the bullets evenly across the graph by using their bulletID values.
    public float deltaX; //delta x
    public float zPos = 0; //starting at 0 for now. Later on I'll spawn the bullets evenly across the graph by using their bulletID values.
    public float deltaZ; //delta z


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        bool gameOver = GameObject.Find("Player").GetComponent<CollisionDetection>().gameOver;
        if (!gameOver) //checks to see if the game is still running.
        {
            if (patternType == 1)
            {
                switch(graphIndex)
                {
                    case 0:
                        deltaX = epsilon;
                        deltaZ = epsilon;
                        xPos = xPos + (deltaX * speed * directionModifier * Time.deltaTime);
                        zPos = zPos + (deltaZ * speed * directionModifier * Time.deltaTime);
                        break;
                    case 1:
                        goto case 0;
                    default:
                        Debug.Log("error: no case specified for graph type " + graphRectList[graphIndex]);
                        break;
                }
                deltaX = xPos-transform.position.x;
                deltaZ = zPos-transform.position.z;
                transform.Translate(new Vector3(deltaX, 0, deltaZ));
                boundCheck();
            }
            else if (patternType == 2)
            {
                switch(graphIndex)
                {
                    case 0: //SinRose4
                        deltaTheta = epsilon * speed * Time.deltaTime * Mathf.PI;
                        theta += deltaTheta; //* directionModifier;
                        radius = Mathf.Sin(2*theta) * size;
                        break;

                    case 1: //Cos Cross
                        deltaTheta = epsilon * speed * Time.deltaTime * Mathf.PI;
                        theta += deltaTheta; //* directionModifier;
                        radius = Mathf.Cos((3.0f/5.0f)*theta) * size;
                        break;
                        //goto case 0;

                    case 2: //SinRose10
                        deltaTheta = epsilon * speed * Time.deltaTime * Mathf.PI;
                        theta += deltaTheta; //* directionModifier;
                        radius = Mathf.Abs(Mathf.Sin(4*theta)) * size;
                        break;

                    default:
                        Debug.Log("error: no case specified for graph type " + graphPolList[graphIndex]);
                        break;
                }
            xPos = radius*Mathf.Cos(theta);
            zPos = radius*Mathf.Sin(theta);
            deltaX = xPos-transform.localPosition.x;
            deltaZ = zPos-transform.localPosition.z;
            transform.Translate(new Vector3(deltaX, 0, deltaZ));
            }
            // else
            // {
            //     Debug.Log("error: invalid patternType");
            // }
        }
    }
    void boundCheck()
    {
        if (xPos > xBound || xPos < -xBound)
        {
            deltaX *= -1;
        }
        if (zPos > zBound || zPos < -zBound)
        {
            deltaZ *= -1;
        }
    }
}
