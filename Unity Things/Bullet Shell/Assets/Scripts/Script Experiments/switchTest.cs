using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchTest : MonoBehaviour
{

/*
    NOTES ON SWITCH STATEMENTS

  - Format:
        switch(input)
        {
            case //EXPECTED INPUT VALUE HERE
                //CODE HERE
                break //From what I understand, switch statements need you to tell it where to give control next. The ways I know to do this are break (sends control one level back a nest), return (break, but you can return a value), goto (gives control to the specified label)            
            default //Optional. This is what runs if none of the specified cases apply.
                //CODE HERE
                //TRANSFER CONTROL HERE
        }
  - 
    
    
    
    
    
    
    
    
    
*/





    public string button;
    public float arrow;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("output is " + AssigningToSwitchTest(1));
    }

    // Update is called once per frame
    void Update()
    {
        
        arrow = ( -Input.GetAxisRaw("Vertical") + 1 ) / 2 + 1; //GetAxisRaw() on a keyboard returns -1,0, or 1, so this little formula just converts it to all positives: 1,1.5,2.
        if ( arrow != 1.5 )
        {
            switch(arrow)
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
            arrow = 0;
        }
        // button = null;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            button = "one";
            switch(button)
            {
                case "one":
                    Debug.Log("one");
                    Debug.Log(button);
                    break;
            }

        }
    }


    int AssigningToSwitchTest( int choice ) //Note To Self: This function was meant to test if you could return values from inside switch statements. After testing it a bit, it seems you cannot. Instead, you just have to store the desired value in some variable, then return the value from outside the switch statement.
    {
        int output;
        switch(choice)
        {
            case 1: 
                output = 1;
                break;
            case 2: 
                output = 2;
                break;
            default:
                output = 0;
                break;
        }
        return output;
    }
}

