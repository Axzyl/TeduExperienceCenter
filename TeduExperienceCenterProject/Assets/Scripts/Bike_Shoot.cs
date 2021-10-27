using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bike_Shoot : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] Transform shootPoint;
    [SerializeField] AudioClip shootClip;

    bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameSettings.hoverbike) this.enabled = false;
        shootPoint = GameObject.Find("ShootPointPlayer").transform;
    }

    private void Update()
    {

        if (Input.GetButton("Fire1") && canFire && !PlayerHealth.dead)
        {
            GameObject proj = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            StartCoroutine(FireDelay());
            GetComponent<AudioSource>().PlayOneShot(shootClip);
            //proj.GetComponent<Rigidbody>().velocity = shootPoint.forward * 400;
            Destroy(proj, 10);
        }
    }

    IEnumerator FireDelay()
    {
        canFire = false;
        yield return new WaitForSeconds(0.05f);
        canFire = true;
    }

}
