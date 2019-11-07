using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    private float speed = 1.0f;
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();        
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
     float forwardInput = Input.GetAxisRaw("Vertical");

     playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
    }
}
