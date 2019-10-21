using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (gameObject.GetComponent<PlayerControl>().hidden)
        {
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Hit");
            //Destroy(gameObject);
        }
    }
}
