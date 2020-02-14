using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    Rigidbody2D enemyRB;
    SpriteRenderer enemySpriteRend;
    Animator enemyAnim;
    ParticleSystem enemyparticle;
    AudioSource enemyAudio;

    float timeBeforeChange;
    public float delay = 0.5f;
    public float speed = 0.3f;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySpriteRend = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        enemyparticle = GameObject.Find("EnemyParticle").GetComponent<ParticleSystem>();
        enemyAudio = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.velocity = Vector2.right * speed;

        if (speed < 0)
        {
            enemySpriteRend.flipX = true;
        }
        else if (speed > 0)
            enemySpriteRend.flipX = false;

        if (timeBeforeChange < Time.time)
        {
            speed *= -1;
            timeBeforeChange = Time.time + delay;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.position.y + 0.3f < other.transform.position.y)
            {

                enemyAnim.SetBool("isDead", true);

            }
        }
    }

    public void DesableEnemy()
    {
        enemyAudio.Play();
        enemyparticle.transform.position = transform.position;
        enemyparticle.Play();
        gameObject.SetActive(false);
        //* Destroy(gameObject); eliminando de la herarquia 
    }

}
