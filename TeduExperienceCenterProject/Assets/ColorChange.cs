using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    Renderer ren;

    private void Start()
    {
        ren = GetComponent<Renderer>();
    }

    public void ChangeToRed()
    {
        ren.material.color = Color.red;
    }

    public void ChangeToYellow()
    {
        ren.material.color = Color.yellow;
    }

    public void ChangeToGreen()
    {
        ren.material.color = Color.green;
    }

    public void ChangeToBlue()
    {
        ren.material.color = Color.blue;
    }
}
