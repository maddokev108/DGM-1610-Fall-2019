using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public bool grounded = true;
    public float gravityModifier = 2;
    public bool gameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = Physics.gravity * gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerRb.AddForce(Vector3.up * 12.5f, ForceMode.Impulse);
            grounded = false;
        }
    }
    private void OnCollisionEnter (Collision collision){
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }
}
