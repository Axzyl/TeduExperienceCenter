using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioClip onFire;

    public float damage;

    private void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(onFire);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject hitFX = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(hitFX, 2);

        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
