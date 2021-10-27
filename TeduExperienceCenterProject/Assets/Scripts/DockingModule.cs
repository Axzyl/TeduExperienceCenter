using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockingModule : MonoBehaviour
{
    [SerializeField] Transform camAdjust;
    [SerializeField] Transform target;
    [SerializeField] Text[] texts;

    Rigidbody rb;
    public Vector3 velocity = Vector3.zero;
    Vector3 rotationVelocity = Vector3.zero;
    float movementDamper = 4;
    float rotationDamper = 3;

    bool[] buttonPressed = new bool[2];

    SpaceStationGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camAdjust.localPosition = -camAdjust.GetChild(0).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) velocity += Vector3.forward / movementDamper;
        if (Input.GetKeyDown(KeyCode.A)) velocity += Vector3.left / movementDamper;
        if (Input.GetKeyDown(KeyCode.S)) velocity += Vector3.back / movementDamper;
        if (Input.GetKeyDown(KeyCode.D)) velocity += Vector3.right / movementDamper;
        if (Input.GetKeyDown(KeyCode.Space)) velocity += Vector3.up / movementDamper;
        if (Input.GetKeyDown(KeyCode.LeftShift)) velocity += Vector3.down / movementDamper;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) rotationVelocity += Vector3.down * rotationDamper;
        if (Input.GetKeyDown(KeyCode.RightArrow)) rotationVelocity += Vector3.up * rotationDamper;
        if (Input.GetKeyDown(KeyCode.UpArrow)) rotationVelocity += Vector3.left * rotationDamper;
        if (Input.GetKeyDown(KeyCode.DownArrow)) rotationVelocity += Vector3.right * rotationDamper;
        if (Input.GetKeyDown(KeyCode.PageUp)) rotationVelocity += Vector3.back * rotationDamper;
        if (Input.GetKeyDown(KeyCode.PageDown)) rotationVelocity += Vector3.forward * rotationDamper;

        if (Input.GetAxis("Axis6") == 1 && !buttonPressed[1])
        {
            velocity += Vector3.up / movementDamper;
            buttonPressed[1] = true;
        }
        if (Input.GetAxis("Axis6") == -1 && !buttonPressed[1])
        {
            velocity += Vector3.down / movementDamper;
            buttonPressed[1] = true;
        }
        if (Input.GetAxis("Axis5") == -1 && !buttonPressed[0])
        {
            velocity += Vector3.left / movementDamper;
            buttonPressed[0] = true;
        }
        if (Input.GetAxis("Axis5") == 1 && !buttonPressed[0])
        {
            velocity += Vector3.right / movementDamper;
            buttonPressed[0] = true;
        }

        if (Input.GetAxis("Axis5") == 0) buttonPressed[0] = false;
        if (Input.GetAxis("Axis6") == 0) buttonPressed[1] = false;

        if(Input.GetButton("Fire2")) velocity = new Vector3(velocity.x,velocity.y, -((Input.GetAxis("Axis4") - 1) / -2f));
        else velocity = new Vector3(velocity.x, velocity.y, (Input.GetAxis("Axis4") - 1) / -2f);

        rotationVelocity = new Vector3(0, Input.GetAxis("Axis3"), 0) * 2;
        if (Input.GetButton("JB4")) velocity = new Vector3(-1, velocity.y, velocity.z);
        //Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical") + " " + Input.GetAxis("Axis3") + " " + Input.GetAxis("Axis4") + " " + Input.GetAxis("Axis5") + " " + Input.GetAxis("Axis6") + " " + Input.GetAxis("Axis7"));

        //Vector3 force = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime;
        rb.velocity = (transform.forward * velocity.z) + (transform.right * velocity.x) + (transform.up * velocity.y);

        //rb.velocity = (transform.forward * (Input.GetAxis("Axis4") - 1) / -2f) + (transform.right * Input.GetAxis("Axis5")) + (transform.up * Input.GetAxis("Axis6"));
        target.eulerAngles += rotationVelocity * Time.deltaTime;
        rotationVelocity = new Vector3(-Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")) * 2;
        transform.eulerAngles += rotationVelocity * Time.deltaTime;
        int maxStringLength = 4;

        texts[0].text = "Position\nX:" + (transform.GetChild(0).position.x - target.position.x).ToString().Substring(0, Mathf.Min((transform.GetChild(0).position.x - target.position.x).ToString().Length, (transform.GetChild(0).position.x - target.position.x) < 0 ? maxStringLength + 1 : maxStringLength)) + "m\n" +
            "Y:" + (target.position.y - transform.GetChild(0).position.y).ToString().Substring(0, Mathf.Min((target.position.y - transform.GetChild(0).position.y).ToString().Length, (target.position.y - transform.GetChild(0).position.y) < 0 ? maxStringLength + 1 : maxStringLength)) + "m\n" +
            "Z:" + (target.position.z - transform.GetChild(0).position.z).ToString().Substring(0, Mathf.Min((target.position.z - transform.GetChild(0).position.z).ToString().Length, (target.position.z - transform.GetChild(0).position.z) < 0 ? maxStringLength + 1 : maxStringLength)) + "m";

        texts[1].text = "Speed\nX:" + (rb.velocity.x).ToString().Substring(0, Mathf.Min((rb.velocity.x).ToString().Length, (rb.velocity.x) < 0 ? maxStringLength + 1 : maxStringLength)) + "m/s\n" +
            "Y:" + (rb.velocity.y).ToString().Substring(0, Mathf.Min((rb.velocity.y).ToString().Length, (rb.velocity.y) < 0 ? maxStringLength + 1 : maxStringLength)) + "m/s\n" +
            "Z:" + (rb.velocity.z).ToString().Substring(0, Mathf.Min((rb.velocity.z).ToString().Length, (rb.velocity.z) < 0 ? maxStringLength + 1 : maxStringLength)) + "m/s\n" +
            "Overall:" + (rb.velocity.magnitude).ToString().Substring(0, Mathf.Min((rb.velocity.magnitude).ToString().Length, (rb.velocity.magnitude) < 0 ? maxStringLength + 1 : maxStringLength)) + "m/s";

        Vector3 angles = transform.eulerAngles;
        if (angles.x > 180) angles -= Vector3.right * 360;
        if (angles.y > 180) angles -= Vector3.up * 360;
        if (angles.z > 180) angles -= Vector3.forward * 360;

        //texts[2].text = "P:" + angles.x.ToString().Substring(0, Mathf.Min(angles.x.ToString().Length, angles.x < 0 ? maxStringLength + 1 : maxStringLength)) + "\n" +
        //    "Y:" + (angles.y).ToString().Substring(0, Mathf.Min(angles.y.ToString().Length, angles.y < 0 ? maxStringLength + 1 : maxStringLength)) + "\n" +
        //    "R:" + (angles.z).ToString().Substring(0, Mathf.Min(angles.z.ToString().Length, angles.z < 0 ? maxStringLength + 1 : maxStringLength));

        //texts[3].text = "P:" + (rotationVelocity.x).ToString().Substring(0, Mathf.Min(rotationVelocity.x.ToString().Length, rotationVelocity.x < 0 ? maxStringLength + 1 : maxStringLength)) + "\n" +
        //    "Y:" + (rotationVelocity.y).ToString().Substring(0, Mathf.Min(rotationVelocity.y.ToString().Length, rotationVelocity.y < 0 ? maxStringLength + 1 : maxStringLength)) + "\n" +
        //    "R:" + (rotationVelocity.z).ToString().Substring(0, Mathf.Min(rotationVelocity.z.ToString().Length, rotationVelocity.z < 0 ? maxStringLength + 1 : maxStringLength));

        texts[2].text = "Yaw\n" + (angles.y).ToString().Substring(0, Mathf.Min(angles.y.ToString().Length, angles.y < 0 ? maxStringLength + 1 : maxStringLength)) + "°\n" +
           (rotationVelocity.y).ToString().Substring(0, Mathf.Min(rotationVelocity.y.ToString().Length, rotationVelocity.y < 0 ? maxStringLength + 1 : maxStringLength)) + "°/s";

        texts[3].text = "Pitch\n" + angles.x.ToString().Substring(0, Mathf.Min(angles.x.ToString().Length, angles.x < 0 ? maxStringLength + 1 : maxStringLength)) + "°\n" +
            (rotationVelocity.x).ToString().Substring(0, Mathf.Min(rotationVelocity.x.ToString().Length, rotationVelocity.x < 0 ? maxStringLength + 1 : maxStringLength)) + "°/s";

        texts[4].text = "Roll\n" + (angles.z).ToString().Substring(0, Mathf.Min(angles.z.ToString().Length, angles.z < 0 ? maxStringLength + 1 : maxStringLength)) + "°\n" +
            (rotationVelocity.z).ToString().Substring(0, Mathf.Min(rotationVelocity.z.ToString().Length, rotationVelocity.z < 0 ? maxStringLength + 1 : maxStringLength)) + "°/s";



    }

    public void ChangePositionVelocity(int movementID)
    {
        if (movementID == 0) velocity += Vector3.forward / movementDamper;
        if (movementID == 1) velocity += Vector3.left / movementDamper;
        if (movementID == 2) velocity += Vector3.back / movementDamper;
        if (movementID == 3) velocity += Vector3.right / movementDamper;
        if (movementID == 4) velocity += Vector3.up / movementDamper;
        if (movementID == 5) velocity += Vector3.down / movementDamper;
    }

    public void ChangeRotationVelocity(int movementID)
    {
        if (movementID == 0) rotationVelocity += Vector3.down * rotationDamper;
        if (movementID == 1) rotationVelocity += Vector3.up * rotationDamper;
        if (movementID == 2) rotationVelocity += Vector3.left * rotationDamper;
        if (movementID == 3) rotationVelocity += Vector3.right * rotationDamper;
        if (movementID == 4) rotationVelocity += Vector3.back * rotationDamper;
        if (movementID == 5) rotationVelocity += Vector3.forward * rotationDamper;
    }
}
