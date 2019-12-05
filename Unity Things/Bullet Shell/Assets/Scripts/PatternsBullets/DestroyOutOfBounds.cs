using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyOutOfBounds : MonoBehaviour
{
    private float xBound = 29.35f; //x boundaries (located outside the pattern spawn zone)
    private float zBound = 22.1f; //z boundaries (located outside the pattern spawn zone)

    // Update is called once per frame
    void Update()
    {                                                                                                                                   
        if ( Mathf.Abs(transform.position.x) > xBound || Mathf.Abs(transform.position.z) > zBound ) //If the object is out of bounds...
        {
            Destroy(gameObject); //... Exterminate! Exterminate!    //     _n_n_
        }                                                           //    /   l--C0
    }                                                               //    |=====|
}                                                                   //    |___L-----CO
//                                                                  //    |O O O D
//                                                                  //    |O O O D\
//                                                                  //    |O O O O D
//                                                                  //    |O_O_O_O_D|
//                                                                  //    L__________\