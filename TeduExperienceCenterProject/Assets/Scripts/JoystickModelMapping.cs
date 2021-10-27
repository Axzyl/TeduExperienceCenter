using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickModelMapping : MonoBehaviour
{
    [SerializeField] float maxAngle;
    [SerializeField] Transform thumbstick;
    [SerializeField] float thumbstickDistance;

    Vector3 initThumbstickPos;

    // Start is called before the first frame update
    void Start()
    {
        initThumbstickPos = thumbstick.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Axis3"), -Input.GetAxis("Horizontal")) * maxAngle;
        thumbstick.localPosition = initThumbstickPos + thumbstick.up * Input.GetAxis("Axis6") * thumbstickDistance + thumbstick.right * Input.GetAxis("Axis5") * thumbstickDistance;
    }
}
