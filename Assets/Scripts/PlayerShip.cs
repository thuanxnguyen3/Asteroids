using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trailLeft = null;
    [SerializeField] TrailRenderer _trailRight = null;
    public GameObject endScreenMenu;
    public GameObject explosion;
    public float invincibilityLength;
    private float invincibilityCounter;
    public GameObject art;
    private float flashCounter;
    public float flashLength = 0.1f;
    public GameObject shield;


    Rigidbody _rb = null;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletForce;

    [Header("Lives")]
    public int score;
    public int lives;

    [Header("UI")]
    public Text scoreText;
    public Text livesText;

    //[Header("Audio")]
    //public AudioSource audioPlayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _trailLeft.enabled = false;
        _trailRight.enabled = false;
    }

    private void Start()
    {
        score = 0;
        lives = 3;

        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;

        DeactivateShield();

    }

    private void Update()
    {
        //check for input from the fire key and make bullets
        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletForce);
            Destroy(newBullet, 5.0f);
        }
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                if (art.activeSelf == true)
                {
                    art.SetActive(false);
                } else if (art.activeSelf == false )
                {
                    art.SetActive(true);
                }
                flashCounter = flashLength;
            }
            if(invincibilityCounter <= 0)
            {
                art.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void MoveShip()
    {
        // s/down = -1, w/up = 1, none = 0, scale by mveSpd
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine our direction w/ our calculated amt
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // apply the mvmt to the physics obj
        _rb.AddForce(moveDirection);

    }
    private void TurnShip()
    {
        // a/left = -1, d/right = 1, none = 0, scale by trnSpd
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply our turn amt (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // spin the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }

    public void SetSpeed(float speedChange)
    {
        _moveSpeed += speedChange;
        //TODO audio/visuals
    }

    public void SetBoosters(bool activeState)
    {
        _trailLeft.enabled = activeState;
        _trailRight.enabled = activeState;
    }
    
    public void ActivateShield()
    {
        shield.SetActive(true);
    }

    public void DeactivateShield()
    {
        shield.SetActive(false);
    }

    public bool HasShield ()
    {
        return shield.activeSelf;
    }



    private void OnCollisionEnter(Collision collision)
    {
        // collision with asteroid
        
        if(collision.relativeVelocity.magnitude > 0)
        {
            if (HasShield())
            {
                DeactivateShield();
            } else { 
                if (invincibilityCounter <= 0)
                {
                    lives--;
                    GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(newExplosion, 5f);
                    livesText.text = "Lives: " + lives;
                    //Player invulnerable after losing a life
                    invincibilityCounter = invincibilityLength;
                    art.SetActive(false);
                    flashCounter = flashLength;

                    if (lives <= 0)
                    {
                        GameOver();
                    }
                }
            }
        } 
    }

 

    public void GameOver()
    {
        endScreenMenu.SetActive(true);
        gameObject.SetActive(false);
        FindObjectOfType<GameInput>().DeathAudio();
    }

    public void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
    }

}
