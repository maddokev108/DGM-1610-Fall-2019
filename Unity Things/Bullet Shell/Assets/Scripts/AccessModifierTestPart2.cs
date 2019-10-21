using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessModifierTestPart2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.parent.GetComponent<AccessModTest>().internalString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
