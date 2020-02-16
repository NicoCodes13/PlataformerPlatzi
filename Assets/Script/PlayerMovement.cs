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
    public bool inFloor = false;

    public bool isGrounded = true;

    public Animator playerAnim;
    AudioSource playerSounds;
    AudioSource steps;

    // Start is called before the first frame update
    void Start()
    {
        steps = GameObject.Find("FootStepsPlayer").GetComponent<AudioSource>();
        playerSounds = GetComponent<AudioSource>();
        steps.Play();
    }

    // Update is called once per frame
    void Update()
    {
        SoundSteps();
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < maxJumps)
        {
            SaltoDoble();
        }
        Animacion();

    }

    private void Animacion()
    {
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
    }

    private void SaltoDoble()
    {

        playerSounds.Play();
        playerRb.AddForce(Vector2.up * jumpForce);
        jumpCounter += 1;
        playerAnim.SetTrigger("Jump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other) //*detecta la colison contra otro Rb
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            inFloor = true;
            jumpCounter = 0;
            maxJumps = 2;
            jumpForce = 450;
        }
    }

    void OnCollisionExit2D(Collision2D other) //* Detecta la ausencia de colisiones 
    {
        inFloor = false;
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpForce = jumpForce / 2;
            if (jumpCounter == 1) //*la falta de colision es por que se realizo un salto?
                maxJumps = 2;
            else
                maxJumps = 1; //* si no fue un salto solo puedes saltar una vez en el aire 
        }
    }

    void SoundSteps()
    {
        if (Input.GetAxis("Horizontal") != 0 && jumpCounter == 0 && maxJumps == 2 && inFloor)
        {
            steps.mute = false;

        }
        else
        {
            steps.mute = true;
        }

    }
}