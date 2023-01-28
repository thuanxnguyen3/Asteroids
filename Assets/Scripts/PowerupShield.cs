using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupShield : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float _powerupDuration = 5f;




    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    [SerializeField] ParticleSystem _collectParticle;


    [Header("Audio")]
    public AudioSource audioPlayer;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we have a valid player and not already powered up
        if (playerShip != null && _poweredUp == false)
        {
            // start powerup timer. Restart, if it's already started
            StartCoroutine(PowerupSequence(playerShip));
        }
        if (other.gameObject.tag == "Player")
        {

            audioPlayer.Play();

        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        // set boolean for detecting lockout
        _poweredUp = true;

        ActivatePowerup(playerShip);
        // simulate this obj being disabled. We don't 
        // really want to disable it, bc we still need
        // script behavior to continue functioning
        DisableObject();

        // wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        // reset
        DeactivatePowerup(playerShip);
        EnableObject();

        // set boolean to release lockout
        _poweredUp = false;

    }


    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            // powerup player
            playerShip.ActivateShield();

        }
        // spawn collect particle
        if (_collectParticle != null)
        {
            Instantiate(_collectParticle, transform.position, transform.rotation);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            playerShip.DeactivateShield();
        }
    }
    public void DisableObject()
    {
        // disable collider, so it can't be retriggered
        _colliderToDeactivate.enabled = false;
        // disable visuals, to simulate deactivated
        _visualsToDeactivate.SetActive(false);
        //TODO reactivavte particle flash/audio
    }

    public void EnableObject()
    {
        // enable collider, so it can be retriggered
        _colliderToDeactivate.enabled = true;
        // enable visuals again, to draw player attention
        _visualsToDeactivate.SetActive(true);
        //TODO reactivate particle flash/audio
    }
}
