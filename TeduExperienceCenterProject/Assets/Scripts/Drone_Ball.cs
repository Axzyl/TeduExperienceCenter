using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Ball : Enemy
{

    public float verticalMovementDistance;
    public float verticalMovementTime;
    public float fireDelay;
    GameObject player;
    float sinAngle;
    float prevSinVal;

    public override void Init()
    {
        LeanTween.moveY(gameObject, transform.position.y + verticalMovementDistance, verticalMovementTime).setEaseInOutQuart();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

        if(Vector3.Distance(player.transform.position, transform.position) > 20)
        {
            transform.position += transform.forward * Time.deltaTime * 7;
        }

        float sin = Mathf.Sin(sinAngle);
        transform.position += transform.up * (sin - prevSinVal) * 0.5f;
        sinAngle += 3 * Time.deltaTime;
        prevSinVal = sin;
    }

    IEnumerator Fire()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(fireDelay);
            GameObject proj = Instantiate(Resources.Load<GameObject>("Drone_Ball_Projectile"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            proj.transform.localEulerAngles += Vector3.up * Random.Range(-30f, 30f);
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * 75;
            proj.GetComponent<EnemyProjectile>().damage = 7;
            Destroy(proj, 20);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
}
