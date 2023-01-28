using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
    GameInput uIController = null;
    [SerializeField] GameObject endScreenMenu;
    private void Awake()
    {
        uIController = FindObjectOfType<GameInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uIController != null)
            {
                endScreenMenu.SetActive(true);
                other.gameObject.SetActive(false);
                FindObjectOfType<GameInput>().WinAudio();
            }
        }
    }
}
