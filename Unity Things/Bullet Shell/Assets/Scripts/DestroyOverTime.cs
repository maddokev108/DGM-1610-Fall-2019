using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifespan = 10.0f;
    public float born;
    // Start is called before the first frame update
    void Start()
    {
        born = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGameOver = GameObject.Find("Player").GetComponent<CollisionDetection>().isGameOver;
        if (!isGameOver) //checks to see if the game is still running.
        {
            if ( Time.time >= lifespan + born )
            {
                Destroy(gameObject);
            }
        }
    }
}
