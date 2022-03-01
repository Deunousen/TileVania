using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] int coinScore = 10;
    [SerializeField] AudioClip coinPickupSound;

    bool wasCollected = false;    // prevent double coin pickup issue that happens rarely

    Vector2 mainCameraPos;

    void Update() 
    {
        mainCameraPos = Camera.main.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(coinScore);
            AudioSource.PlayClipAtPoint(coinPickupSound, mainCameraPos);
            Destroy(gameObject);
        }
    }
}
