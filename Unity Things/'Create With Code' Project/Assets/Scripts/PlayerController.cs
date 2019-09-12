using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This code makes the vehicle move.
        transform.Translate(Vector3.forward*Time.deltaTime*40);   // This will move the vehicle forward by 1 unit along the Z axis.
    }
}
