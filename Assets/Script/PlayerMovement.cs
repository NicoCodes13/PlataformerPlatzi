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

    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
        playerRb.AddForce(new Vector2(0, Input.GetAxis("Jump") * jumpForce));

        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnim.SetBool("isWalking", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnim.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnim.SetBool("isWalking", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }

        // if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        // {
        //     playerRb.AddForce(Vector2.up * jumpForce);
        //     jumpCounter += 1;
        //     playerAnim.SetTrigger("Jump");
        // }
    }

    private void OnCollisionEnter2D(Collision2D other) //*detecta la colison contra otro Rb
    {
        jumpCounter = 0;
        maxJumps = 2;
    }

    void OnCollisionExit2D(Collision2D other) //* Detecta la ausencia de colisiones 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (jumpCounter == 1) //*la falta de colision es por que se realizo un salto?
                maxJumps = 2;
            else
                maxJumps = 1; //* si no fue un salto solo puedes saltar una vez en el aire 
        }
    }

}