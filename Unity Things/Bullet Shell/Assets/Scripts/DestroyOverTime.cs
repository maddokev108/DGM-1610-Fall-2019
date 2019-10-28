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
        if ( Time.time >= lifespan + born )
        {
            Destroy(gameObject);
        }
    }
}
