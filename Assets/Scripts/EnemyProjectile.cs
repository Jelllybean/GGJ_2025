using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 2;
    private float spawnTime;

    private void Awake()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (spawnTime + lifetime < Time.time)
        {
            Disappear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().RemoveHealth(40);
            Disappear();
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
