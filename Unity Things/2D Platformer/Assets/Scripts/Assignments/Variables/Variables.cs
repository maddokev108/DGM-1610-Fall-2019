using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    string thing = "Potama Llama";
    int twelve = 12;
    float kindaTwelve = 12.00000001f;
    bool right = true;
    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The thing is called \"" + thing + ".\"");
        Debug.Log("The number to use when you want a dozen donuts, but the word \"dozen\" is too mainstream: " + twelve);
        Debug.Log("The number to use when you want twelve donuts, but 12 is too mainstream: " + kindaTwelve);
        Debug.Log("When it's not false, it's " + right);
    }

}
