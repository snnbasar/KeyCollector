using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;

    private void Start()
    {
        GameManager.instance.totalKeys++;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        GameManager.instance.collectedKeys++;
        SoundManager.instance.PlayMusic(Soundlar.Collect);
        Destroy(gameObject);
    }
}
