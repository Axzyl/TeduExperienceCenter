using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] AudioClip onFire;

    public float damage;

    private void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(onFire);
    }

    private void OnTriggerREnter(Collider collision)
    {
        GameObject hitFX = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(hitFX, 2);
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
