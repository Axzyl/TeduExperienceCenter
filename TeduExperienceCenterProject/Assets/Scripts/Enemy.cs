using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float health;
    [SerializeField] bool runInitOnStart;
    [SerializeField] float maxHealth;
    [SerializeField] int pointValue;
    [SerializeField] GameObject spawnOnDeath;
    [SerializeField] AudioClip playOnDeath;

    public virtual void Init()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (runInitOnStart) Init();
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0) OnDeath();
    }

    public virtual void OnDeath()
    {
        GameObject.Find("UI").GetComponent<UIUpdater>().UpdatePointsText(pointValue);
        GameObject a = Instantiate(spawnOnDeath, transform.position, transform.rotation);
        GetComponent<AudioSource>().PlayOneShot(playOnDeath);
        Destroy(gameObject);
    }
}
