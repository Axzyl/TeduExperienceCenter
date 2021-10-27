using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class KeepRotation : MonoBehaviour
{
    Vector3 initLocalPos = new Vector3(0.04f,1.9f,-1.98f);
    Vector3 initLocalRot = new Vector3(14.62f, 180f, 0f);

    [SerializeField] Transform roverHead;
    [SerializeField] GameObject carrot;
    [SerializeField] Transform shootPoint;
    [SerializeField] AudioClip shootClip;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSettings.hoverbike) this.enabled = false;
        shootPoint = GameObject.Find("ShootPointPlayer").transform;
        //transform.GetChild(0).parent = null;
    }

    private void Update()
    {

        if(roverHead != null && transform.GetChild(0).GetChild(0).GetChild(0).childCount != 0)
            roverHead.eulerAngles = new Vector3(roverHead.eulerAngles.x, transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).eulerAngles.y, roverHead.eulerAngles.z);

        if (Input.GetButtonDown("Fire1") && !PlayerHealth.dead)
        {
            GameObject proj = Instantiate(carrot, shootPoint.position, shootPoint.rotation);
            GetComponent<AudioSource>().PlayOneShot(shootClip);
            proj.GetComponent<Rigidbody>().velocity = shootPoint.forward * 400;
            Destroy(proj,10);
        }
    }

}
