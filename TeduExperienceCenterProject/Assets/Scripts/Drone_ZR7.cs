using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_ZR7 : Enemy
{
    public float verticalMovementDistance;
    public float verticalMovementTime;
    public float targetVerticalDistance;
    public float randomMovementHorizontal;
    public float movementGain;
    public float fireDelay;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject warning;
    [SerializeField] AudioClip warningAudio;
    [SerializeField] AudioClip laserShot;
    GameObject player;
    float sinAngle;
    float prevSinVal;
    float randMovH;
    float sinMagnitude;
    bool canRotate = true;

    bool laserHitting = false;
    Transform a;

    public override void Init()
    {
        LeanTween.moveY(gameObject, transform.position.y + verticalMovementDistance, verticalMovementTime).setEaseInOutQuart();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Fire());
        StartCoroutine(ChangeFlightDirection());
        a = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        float sin = Mathf.Sin(sinAngle);

        a.transform.LookAt(player.transform);
        //transform.eulerAngles += (a.transform.eulerAngles - transform.eulerAngles) * 0.3f;
        transform.eulerAngles = new Vector3(a.transform.eulerAngles.x,transform.eulerAngles.y + ((a.transform.eulerAngles.y - transform.eulerAngles.y) * 0.5f * Time.deltaTime), a.transform.eulerAngles.z);
        //transform.position += transform.right * (sin - prevSinVal) * sinMagnitude;

        //if (Vector3.Distance(player.transform.position, transform.position) > 30)
        //{
        //    transform.position += transform.forward * Time.deltaTime * 2;
        //}

        //transform.position += transform.right * randMovH * Time.deltaTime;


        transform.position += transform.up * (sin - prevSinVal) * sinMagnitude;
        sinAngle += 3 * Time.deltaTime;
        prevSinVal = sin;
    }

    IEnumerator Fire()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(fireDelay);
            warning.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(warningAudio);
            yield return new WaitForSeconds(1.5f);
            canRotate = false;
            yield return new WaitForSeconds(1.5f);
            warning.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(laserShot);
            laser.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            laser.SetActive(false);
            canRotate = true;
        }
    }

    public void HittingPlayer(PlayerHealth player)
    {
        if (!laserHitting) StartCoroutine(DealDamage(player));
    }

    IEnumerator DealDamage(PlayerHealth player)
    {
        laserHitting = true;
        player.TakeDamage(100);
        yield return new WaitForSeconds(1);
        laserHitting = false;
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

    public override void OnDeath()
    {
        base.OnDeath();
    }
}
