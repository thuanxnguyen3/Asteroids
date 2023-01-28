using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    GameInput uIController = null;
    [SerializeField] GameObject endScreenMenu;
    private void Awake()
    {
        uIController = FindObjectOfType<GameInput>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // detect if it's the player 
        PlayerShip playerShip = other.gameObject.GetComponent<PlayerShip>();
        // if we found something valid, continue
        if (playerShip != null)
        {
            // do something!
            playerShip.Kill();
        }
        other.gameObject.SetActive(false);
        if (uIController != null)
        {
            endScreenMenu.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
