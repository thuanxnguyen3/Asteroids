using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Speed")]
    public float maxThrust;

    [Header("Feedback")]
    public Rigidbody rb;

    [Header("Size")]
    public int asteroidSize;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;

    [Header("Points")]
    public int points;
    public GameObject playerGO;

    [Header("Particle FX")]
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        rb.AddForce(thrust);

        playerGO = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y != 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if(asteroidSize == 3)
            {
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                Instantiate(asteroidMedium, transform.position, transform.rotation);
                asteroidSize = 2;
            } else if (asteroidSize == 2) 
            {
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                asteroidSize = 1;

            }
            else if (asteroidSize == 1)
            {

            }
            //Tell player to score some points
            playerGO.SendMessage("ScorePoints", points);

            //explosion
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newExplosion, 5f);
            //destroy
            Destroy(gameObject);

        }
    }

}
