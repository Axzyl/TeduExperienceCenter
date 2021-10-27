using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverbike : MonoBehaviour
{
    Vector3 currentMovementDirection;
    Rigidbody rb;

    bool startFloating;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartFloat()
    {
        startFloating = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startFloating) return;

        if (!GameSettings.hoverbike) return;

        currentMovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 400;
        transform.eulerAngles += Vector3.up * Time.deltaTime * Input.GetAxis("Axis3") * 90;
        
        if (Input.GetButton("Fire2"))
        {
            rb.drag = 10;
            rb.angularDrag = 10;
        }
        else
        {
            rb.drag = 1;
            rb.angularDrag = 1f;
        }

       
        if (rb.velocity.magnitude <= 25)
        {
            rb.AddForce((transform.forward * currentMovementDirection.z) + (transform.right * currentMovementDirection.x) + (transform.up * Input.GetAxis("Axis6") * 200));
        }
        else
        {
            rb.AddForce(transform.up * Input.GetAxis("Axis6") * 200);
        }
        
        if(rb.constraints != RigidbodyConstraints.FreezeAll)
            transform.GetChild(0).localEulerAngles = new Vector3(transform.InverseTransformDirection(rb.velocity).z, 0, transform.InverseTransformDirection(rb.velocity).x);

        GetComponent<AudioSource>().volume = Mathf.Min(Mathf.Max(Mathf.Abs(transform.InverseTransformDirection(rb.velocity).x), Mathf.Abs(transform.InverseTransformDirection(rb.velocity).y), Mathf.Abs(transform.InverseTransformDirection(rb.velocity).z)) / 3, 1f);


        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Environment")) && startFloating)
        {
            GameObject.Find("UI").GetComponent<UIUpdater>().UpdateAltitudeBar(hit.distance);
        }
    }
}
