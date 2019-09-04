using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions1 : MonoBehaviour
{
    // string firstName;
    // string lastName;
    
    // string date;
    // string item;
    // int charge;
    // string currency;
    
    // bool isRewardsMember;
    

    int num1;
    int num2;
    int total;

    // Start is called before the first frame update
    void Start()
    {
        // firstName = "Grey";
        // lastName = "Goo";
        
        // date = "Aug 7, 3082";
        // item = "2.8 Ml Rocket Fuel";
        // charge = 64;
        // currency = "\"some sort of glowing tablets\"";
        
        // isRewardsMember = false;

        // Customer(firstName, lastName, date, item, charge, currency, isRewardsMember);

        num1 = 5;
        num2 = 2;

        Debug.Log( num1 + " + " + num2 + " = " + calcAdd(num1, num2) );

    }

    int calcAdd(int n1, int n2)
    {
        total = n1 + n2;

        return total;
    }

    // void Customer(string fname, string lname, string dt, string it, int price, string cur, bool member)
    // {
        
    //     Debug.Log(dt + ": Customer " + fname + " " + lname + " purchased " + it + " for " + currency + ".");
    //     Debug.Log("Amount Charged: ");
    //     Debug.Log(price);
        
    //     Debug.Log("Is " + fname + " " + lname + " a rewards member:");
    //     Debug.Log (member);
    // }
}
