using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCheckCollision : MonoBehaviour
{
    public bool checkCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (!checkCollision) return;

        GameObject[] armComponents = GameObject.FindGameObjectsWithTag("ArmIK");

        foreach(GameObject arm in armComponents)
        {
            arm.GetComponent<CalcIK.CalcIK>().SetColliding(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!checkCollision) return;

        GameObject[] armComponents = GameObject.FindGameObjectsWithTag("ArmIK");
        foreach (GameObject arm in armComponents)
        {
            arm.GetComponent<CalcIK.CalcIK>().SetColliding(false);
        }
    }
}
