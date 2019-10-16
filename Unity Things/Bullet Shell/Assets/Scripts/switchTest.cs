using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float button = ( Input.GetAxisRaw("Horizontal") + 1 ) / 2 + 1;
        if ( button != 0 )
        {
            upDown(button);
            button = 0;
        }
    }
    void upDown(float input){
        switch (input)
        {
            case 1:
                Debug.Log("up");
                break;
            case 2:
                Debug.Log("down");
                break;
            default:
                Debug.Log("Something went wrong.");
                break;

        }
    }
}
