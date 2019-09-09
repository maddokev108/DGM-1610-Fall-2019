using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bananas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(bonusBanana("That's right! 5 LB of sugar, for only $99.99!"));
    }

}

str bonusBanana (str in)
    {
        Debug.Log("Distributing bonus...");
        return (in+" And a FREE banana!");
    }
