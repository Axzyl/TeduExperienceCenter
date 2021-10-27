using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Insect : Enemy
{
    public float verticalMovementDistance;
    public float verticalMovementTime;
    public float randomMovementHorizontal;
    public float fireDelay;
    GameObject player;
    float sinAngle;
    float prevSinVal;
    float randMovH;
    float sinMagnitude;

    public override void Init()
    {
        LeanTween.moveY(gameObject, transform.position.y + verticalMovementDistance, verticalMovementTime).setEaseInOutQuart();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChangeFlightDirection());
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        if (Vector3.Distance(player.transform.position, transform.position) > 20)
        {
            transform.position += transform.forward * Time.deltaTime * 4;
        }
        
        transform.position += transform.right * randMovH * Time.deltaTime;
        
        float sin = Mathf.Sin(sinAngle);
        transform.position += transform.up * (sin - prevSinVal) * sinMagnitude;
        sinAngle += 3 * Time.deltaTime;
        prevSinVal = sin;
    }

    IEnumerator Fire()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(fireDelay);
            for(int i = 0; i < 4; i++)
            {
                GameObject proj = Instantiate(Resources.Load<GameObject>("Drone_Insect_Projectile"), transform.GetChild(0).position, transform.GetChild(0).rotation);
                proj.transform.localEulerAngles += Vector3.up * Random.Range(-60f,60f);
                proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * 50;
                proj.GetComponent<EnemyProjectile>().damage = 4;
                Destroy(proj, 20);
                yield return new WaitForSeconds(0.05f);
            }
            
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    IEnumerator ChangeFlightDirection()
    {
        while (gameObject.activeSelf)
        {
            randMovH = Random.Range(randomMovementHorizontal / 1.5f, randomMovementHorizontal * 1.5f);
            if (Random.Range(1, 100) > 50) randMovH = -randMovH;
            sinMagnitude = Random.Range(1f, 3f);
            yield return new WaitForSeconds(Random.Range(2f, 3f));
        }
    }
}
