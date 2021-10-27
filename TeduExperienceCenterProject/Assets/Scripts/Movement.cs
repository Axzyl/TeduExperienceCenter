using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Vector3 movementAngle;
    float rotationX;
    float rotationY;

    [SerializeField] Transform cam;
    [SerializeField] GameObject carrot;
    [SerializeField] float maxHealth;

    float health;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        movementAngle = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementAngle += transform.forward * 10;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementAngle += transform.right * -10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementAngle += transform.forward * -10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementAngle += transform.right * 10;
        }

        rotationX += -Input.GetAxis("Mouse Y") * 8;
        rotationX = Mathf.Clamp(rotationX, -80, 80);
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 8, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementAngle *= 3;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity += Vector3.up * 20 * Time.deltaTime;
        }
        GetComponent<Rigidbody>().velocity = new Vector3(movementAngle.x, GetComponent<Rigidbody>().velocity.y, movementAngle.z);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject proj = Instantiate(carrot, cam.position + (cam.forward * 1.5f), cam.rotation);
            proj.GetComponent<Rigidbody>().velocity = proj.transform.forward * 300;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("HP: " + health);
        if(health <= 0)
        {
            Debug.Log("rip");
        }
    }
}
