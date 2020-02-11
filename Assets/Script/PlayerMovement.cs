using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D playerRb;
    public float speed = 2;
    public float jumpForce = 300;

    int jumpCounter = 0;
    int maxJumps = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        {
            playerRb.AddForce(Vector2.up * jumpForce);
            jumpCounter += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //*detecta la colison contra otro Rb
    {
        jumpCounter = 0;
        maxJumps = 2;
    }

    void OnCollisionExit2D(Collision2D other) //* Detecta la ausencia de colisiones 
    {
        if (jumpCounter == 1) //*la falta de colision es por que se realizo un salto?
            maxJumps = 2;
        else
            maxJumps = 1; //* si no fue un salto solo puedes saltar una vez en el aire 
    }

}