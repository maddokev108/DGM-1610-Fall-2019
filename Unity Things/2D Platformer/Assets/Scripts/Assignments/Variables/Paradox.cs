using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paradox : MonoBehaviour 
{ 
    string brainhurt = "This sentence is false."; 
    int bestNum = 7; 
     
 
    // Start is called before the first frame update 
    void Start() 
    { 
        Debug.Log(brainhurt); 
        Debug.Log(bestNum*bestNum-bestNum); 
    } 
 
} 

