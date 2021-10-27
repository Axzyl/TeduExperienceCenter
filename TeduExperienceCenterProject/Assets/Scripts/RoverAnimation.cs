using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverAnimation : MonoBehaviour
{

    [SerializeField] GameObject ramp;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject[] solarPanels;
    [SerializeField] GameObject antenna;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        LeanTween.moveLocalY(ramp, -0.643f, 4f);
        yield return new WaitForSeconds(4f);
        LeanTween.rotateLocal(ramp, new Vector3(109, 0, 0), 1.5f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.rotateZ(cam, 0, 3f);
        yield return new WaitForSeconds(3f);

        LeanTween.rotate(solarPanels[0], new Vector3(180f, 28.3f, 90), 5f);
        LeanTween.rotate(solarPanels[1], new Vector3(180f, -30, -90), 5f);
        yield return new WaitForSeconds(5f);

        //LeanTween.rotateLocal(solarPanels[2], new Vector3(270f, 0, -90), 1.5f);
        //LeanTween.rotateLocal(solarPanels[3], new Vector3(-90f, 0, -90), 1.5f);

        solarPanels[2].transform.eulerAngles = new Vector3(90, 90, -90);
        solarPanels[3].transform.eulerAngles = new Vector3(90, 90, -90);

        float angleAdd = 0;
        while (angleAdd < 180)
        {
            angleAdd += Time.deltaTime * 30;
            solarPanels[2].transform.eulerAngles = new Vector3(90 + angleAdd, 90, -90);
            solarPanels[3].transform.eulerAngles = new Vector3(90 - angleAdd, 90, -90);
            yield return new WaitForEndOfFrame();
        }
        
        LeanTween.rotateZ(antenna, 155, 3f);
        yield return new WaitForSeconds(3f);

        transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.GetChild(0).GetComponent<Rigidbody>().velocity = transform.forward * 3;
    }
}
