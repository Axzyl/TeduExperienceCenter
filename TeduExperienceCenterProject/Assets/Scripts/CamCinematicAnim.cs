using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCinematicAnim : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform hoverBikeTarget;

    bool hoverbike;

    float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        hoverbike = GameSettings.hoverbike;
        Debug.Log("aaaa " + hoverbike);
        if(hoverbike) StartCoroutine(AttachToBike());
        else StartCoroutine(AttachToPlayer());
    }

    private void Update()
    {
        //rotationX += -Input.GetAxis("Mouse Y") * 15;
        //rotationX = Mathf.Clamp(rotationX, -90, 90);
        //transform.GetChild(0).localRotation = Quaternion.Euler(rotationX, 0, 0);
        //transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 15, 0);
    }

    IEnumerator AttachToPlayer()
    {
        transform.GetChild(0).GetChild(0).localPosition = -transform.GetChild(0).GetChild(0).GetChild(0).localPosition;
        //transform.GetChild(0).GetChild(0).localEulerAngles = -transform.GetChild(0).GetChild(0).GetChild(0).localEulerAngles;
        LeanTween.move(gameObject, new Vector3(target.position.x, target.position.y, transform.position.z), 10f);
        LeanTween.rotateX(gameObject, 14.62f, 10f);
        LeanTween.rotateY(gameObject, 180f, 10f);
        yield return new WaitForSeconds(10);
        LeanTween.move(gameObject, target.position, 1.5f);
        yield return new WaitForSeconds(1.5f);
        transform.SetParent(target);
        LeanTween.rotateY(gameObject, 0, 2f);
        yield return new WaitForSeconds(2f);
        //transform.GetChild(0).GetChild(0).localPosition = -transform.GetChild(0).GetChild(0).GetChild(0).localPosition;
        //transform.GetChild(0).GetChild(0).localEulerAngles = -transform.GetChild(0).GetChild(0).GetChild(0).localEulerAngles;
        GameObject.Find("Waypoints").GetComponent<WaypointsAndEnemyEvents>().StartTiming();
    }

    IEnumerator AttachToBike()
    {
        transform.GetChild(0).GetChild(0).localPosition = -transform.GetChild(0).GetChild(0).GetChild(0).localPosition;
        //transform.GetChild(0).GetChild(0).localEulerAngles = -transform.GetChild(0).GetChild(0).GetChild(0).localEulerAngles;
        LeanTween.move(gameObject, new Vector3(hoverBikeTarget.position.x, hoverBikeTarget.position.y, hoverBikeTarget.position.z), 10f);
        LeanTween.rotateX(gameObject, 0, 10f);
        LeanTween.rotateY(gameObject, 180f, 10f);
        yield return new WaitForSeconds(10);
        //LeanTween.move(gameObject, target.position, 1.5f);
        //yield return new WaitForSeconds(1.5f);
        transform.SetParent(hoverBikeTarget);
        LeanTween.rotateY(gameObject, 0, 2f);
        yield return new WaitForSeconds(2f);
        //transform.GetChild(0).GetChild(0).localPosition = -transform.GetChild(0).GetChild(0).GetChild(0).localPosition;
        //transform.GetChild(0).GetChild(0).localEulerAngles = -transform.GetChild(0).GetChild(0).GetChild(0).localEulerAngles;
        GameObject.Find("Waypoints").GetComponent<WaypointsAndEnemyEvents>().StartTiming();
    }

    //IEnumerator Aaaaa()
    //{
    //    LeanTween.move(gameObject, target2, 15f).setEaseInCubic();
    //    LeanTween.rotateY(gameObject, 154.5f, 15f).setEaseInCubic();
    //    yield return new WaitForSeconds(15f);
    //    LeanTween.move(gameObject, target3, 15f).setEaseOutCubic();
    //    LeanTween.rotateY(gameObject, 91.6f, 15f).setEaseOutCubic();
    //    yield return new WaitForSeconds(15f);
    //}

}
