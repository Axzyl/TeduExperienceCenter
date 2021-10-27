using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautAnimation : MonoBehaviour
{
    [SerializeField] Transform[] points;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        transform.position = points[0].position;
        transform.LookAt(points[1]);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0); 
        LeanTween.move(gameObject, points[1].position, 15);
        GetComponent<Animator>().Play("Walk");
        yield return new WaitForSeconds(15);
        GetComponent<Animator>().Play("Idle");
    }
}
