using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float xBound = 29.35f; //17.8f * 1.25f + 7.1f   prevents this script from destroying patterns as they spawn in.
    private float zBound = 22.1f; //10.0f * 1.5f + 7.1f   prevents this script from destroying patterns as they spawn in.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Mathf.Abs(transform.position.x) > xBound || Mathf.Abs(transform.position.z) > zBound )
        {
            Destroy(gameObject);
        }
    }
}
