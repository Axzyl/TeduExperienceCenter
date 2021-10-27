using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimation : MonoBehaviour
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
        LeanTween.move(gameObject, points.GetChild(2).position, 2.5f);
        yield return new WaitForSeconds(2.5f);
        anim.Play("Sprint To Wall Climb");
        transform.eulerAngles = new Vector3(0, 90, 0);
        //LeanTween.moveX(gameObject, points.GetChild(3).position.x, 1.5f);
        //LeanTween.moveZ(gameObject, points.GetChild(3).position.z, 1.5f);
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 10, GetComponent<Rigidbody>().velocity.z);
        yield return new WaitForSeconds(1.5f);
        LeanTween.rotateY(gameObject, 0, 0.75f);
        LeanTween.move(gameObject, points.GetChild(3).position, 0.75f);
        yield return new WaitForSeconds(1.75f);
        transform.SetParent(GameObject.Find("hover").transform.GetChild(0));
        GameObject.Find("hover").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("hover").GetComponent<Hoverbike>().StartFloat();

        if (GameSettings.hoverbike)
        {
            GameObject.Find("hover").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            gameObject.SetActive(false);
        }

        //LeanTween.moveY(GameObject.Find("hover"), GameObject.Find("hover").transform.position.y + 9f, 4.5f).setEaseInOutQuart();
        //yield return new WaitForSeconds(6f);
        //LeanTween.rotateX(GameObject.Find("hover").transform.GetChild(0).gameObject, GameObject.Find("hover").transform.GetChild(0).eulerAngles.x + 45, 0.5f);
        //LeanTween.moveZ(GameObject.Find("hover"), GameObject.Find("hover").transform.position.z + 9f, 3f).setEaseInOutQuart();
        //yield return new WaitForSeconds(3);
        //GameObject.Find("hover").transform.GetChild(0).gameObject.SetActive(false);

        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //LeanTween.move(gameObject, points.GetChild(3).position, 1);
        //anim.Play("Chicken Dance");
    }
}
