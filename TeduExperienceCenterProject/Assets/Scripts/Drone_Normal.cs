using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Normal : Enemy
{
    public float verticalMovementDistance;
    public float verticalMovementTime;
    public float targetVerticalDistance;
    public float movementGain;
    public float fireDelay;
    GameObject player;
    float sinAngle;
    float prevSinVal;

    public override void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LeanTween.moveY(gameObject, transform.position.y + verticalMovementDistance, verticalMovementTime).setEaseInOutQuart();
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.LookAt(player.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        targetVerticalDistance = player.transform.position.y;

        Ray ray = new Ray(player.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Environment")))
        {
            targetVerticalDistance = player.transform.position.y - hit.point.y;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 30)
        {
            transform.position += transform.forward * Time.deltaTime * 10;
        }

        ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit2;
        if(Physics.Raycast(ray, out hit2, 1000, LayerMask.GetMask("Environment")))
        {
            if(transform.position.y - player.transform.position.y > transform.position.y - hit.point.y)
            {
                transform.position += Vector3.up * (4 - (transform.position.y - hit.point.y)) * movementGain * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.up * (targetVerticalDistance - (transform.position.y - hit.point.y)) * movementGain * Time.deltaTime;
            }
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
            GameObject proj = Instantiate(Resources.Load<GameObject>("Drone_Normal_Projectile"), transform.GetChild(0).position, transform.GetChild(0).rotation);
            proj.transform.localEulerAngles += Vector3.up * Random.Range(-15f, 15f);
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * 45;
            proj.GetComponent<EnemyProjectile>().damage = 15;
            Destroy(proj, 20);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
}
