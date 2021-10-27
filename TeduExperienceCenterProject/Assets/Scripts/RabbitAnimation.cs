using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAnimation : MonoBehaviour
{

    [SerializeField] Transform points;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        transform.position = points.GetChild(0).position;
        transform.LookAt(points.GetChild(1));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.Play("Walking");
        LeanTween.move(gameObject, points.GetChild(1).position, 5);
        yield return new WaitForSeconds(5);
        transform.LookAt(points.GetChild(2));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        anim.Play("Running");
        LeanTween.move(gameObject, points.GetChild(2).position, 1.5f);
        yield return new WaitForSeconds(1.5f);
        anim.Play("Jump");
        transform.eulerAngles = new Vector3(0, -90, 0);
        LeanTween.moveX(gameObject, points.GetChild(3).position.x, 1.5f);
        LeanTween.moveZ(gameObject, points.GetChild(3).position.z, 1.5f);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 10, GetComponent<Rigidbody>().velocity.z);
        yield return new WaitForSeconds(1.5f);
        LeanTween.rotateY(gameObject, 0, 0.75f);
        transform.SetParent(GameObject.Find("Player").transform);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        LeanTween.move(gameObject, points.GetChild(3).position, 1);
        if (!GameSettings.hoverbike) GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        anim.Play("Chicken Dance");
    }
}
