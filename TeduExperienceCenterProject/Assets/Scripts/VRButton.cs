using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    public UnityEvent OnClick;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("A");
        OnClick.Invoke();
    }
}
