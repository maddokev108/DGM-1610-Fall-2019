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
        if (other.gameObject.CompareTag("Bullet"))
        {

            if (gameObject.GetComponent<PlayerController>().hidden)
            {
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Hit");
                //Destroy(gameObject);
            }
        } else if ( other.gameObject.CompareTag("Pickup") )
        {
            Debug.Log("Pickup collected");
            Destroy(other.gameObject);
        }

    }
}
