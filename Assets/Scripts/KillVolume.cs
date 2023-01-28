using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillVolume : MonoBehaviour
{
    GameInput uIController = null;
    [SerializeField] GameObject endScreenMenu;
    // Start is called before the first frame update
    void Start()
    {
        uIController = FindObjectOfType<GameInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void endScreen()
    {
        PlayerShip playerShip = gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            // do something!
            playerShip.Kill();
        }
        if (uIController != null)
        {
            endScreenMenu.SetActive(true);
        }
    }
}
