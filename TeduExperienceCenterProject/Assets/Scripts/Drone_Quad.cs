using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Quad : Enemy
{
    public float verticalMovementDistance;
    public float verticalMovementTime;
    public float targetDistance;
    GameObject player;
    float sinAngle;
    float prevSinVal;

    public override void Init()
    {
        LeanTween.moveY(gameObject, transform.position.y + verticalMovementDistance, verticalMovementTime).setEaseInOutQuart();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {

        //float sin = Mathf.Sin(sinAngle);
        //transform.position += transform.up * (sin - prevSinVal) * 0.5f;
        //sinAngle += 3 * Time.deltaTime;
        //prevSinVal = sin;
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject proj = Instantiate(Resources.Load<GameObject>("Drone_Quad_Projectile"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            proj.GetComponent<EnemyGrenade>().damage = 20;
            Destroy(proj, 20);
            yield return new WaitForSeconds(0.6f);
        }
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(verticalMovementTime / 2);

        while (gameObject.activeSelf)
        {
            transform.LookAt(player.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            StartCoroutine(Fire());
            LeanTween.move(gameObject, transform.position + (transform.forward * Mathf.Max(Vector3.Distance(transform.position, player.transform.position) * 2,50)), 9);
            LeanTween.moveY(gameObject, transform.position.y + 5, 4.5f);
            yield return new WaitForSeconds(4.5f);
            LeanTween.moveY(gameObject, transform.position.y - 5, 4.5f);
            yield return new WaitForSeconds(5);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
}
