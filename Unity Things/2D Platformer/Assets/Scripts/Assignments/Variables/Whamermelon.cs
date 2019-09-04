using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whamermelon : MonoBehaviour 
{ 
    string thing = "WHAM-ermelon(tm)"; 
    float tvThings = 99.99f; 
    bool bad = false; 
 
 
    // Start is called before the first frame update 
    void Start() 
    { 
        Debug.Log("The thing is a " + thing + ". Probably not safe to eat."); 
        Debug.Log("Buy now and receive a free smiley sticker! That's right, one whole " + thing + " plus a FREE sticker, all for just one easy payment of $"+tvThings+"!"); 
        Debug.Log("True or false: is the as-seen-on-TV \""+thing+"\" a good deal? Find out in the next output line!"); 
		Debug.Log(bad); 
    } 
 
} 

