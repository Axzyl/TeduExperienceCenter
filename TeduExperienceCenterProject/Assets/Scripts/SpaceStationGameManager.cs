using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceStationGameManager : MonoBehaviour
{
    public static int stage;

    [SerializeField] int startingStage;

    [SerializeField] RawImage screen;
    [SerializeField] GameObject[] staticEffect;
    [SerializeField] RenderTexture[] renderers;     
    [SerializeField] GameObject[] UI;
    [SerializeField] GameObject dockingModule;
    [SerializeField] GameObject arm;
    [SerializeField] Transform[] armTargets;
    [SerializeField] Transform attachToModule;
    [SerializeField] GameObject[] armDockAttachers;
    [SerializeField] GameObject crosshair;
    [SerializeField] SpaceStationDialogue dialogue;
    [SerializeField] Transform camAdjust;

    int a = 0;

    // Start is called before the first frame update
    void Start()
    {
        stage = startingStage;
        if (stage == 1) SetupStage1();
        if(stage == 2) SetupStage2();
        if(stage == 3) SetupStage3();

    }

    // Update is called once per frame
    void Update()
    {
        //if (a == 0)
        //{
        //    a += 1;
        //    camAdjust.GetChild(0).GetChild(0).GetChild(0).localPosition = -camAdjust.GetChild(0).GetChild(0).GetChild(0).GetChild(0).localPosition;
        //}
        if (stage == 1)
        {
            // Check win conditions
            if(dockingModule.transform.GetChild(0).position.z - dockingModule.transform.parent.position.z >= 0)
            {
                stage = -1;
                dockingModule.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                if (Stage1WinConditions())
                {
                    Debug.Log("You Win");
                    staticEffect[0].gameObject.SetActive(true);
                    StartCoroutine(StartStageDelay(2, 5));
                }
                else
                {
                    Debug.Log("The space station crashed oh no what are we going to do aaa we are dead");
                    staticEffect[1].gameObject.SetActive(true);
                    StartCoroutine(StartStageDelay(1, 5));
                }
            }
        }

        if(stage == 2)
        {
            Vector3 distance = arm.GetComponent<CalcIK.CalcIK>().GetDistanceToTarget();
            float rot = arm.GetComponent<CalcIK.CalcIK>().GetRotToTarget();
            if (Mathf.Abs(distance.x) < 0.05f && Mathf.Abs(distance.y) < 0.1f &&  Mathf.Abs(distance.z) < 0.05f && Mathf.Abs(rot) < 1){
                stage = -1;
                staticEffect[0].gameObject.SetActive(true);
                Debug.Log("You Win");
                StartCoroutine(StartStageDelay(3, 5));
            }
        }

        if(stage == 3)
        {
            Vector3 distance = arm.GetComponent<CalcIK.CalcIK>().GetDistanceToTarget();
            float rot = arm.GetComponent<CalcIK.CalcIK>().GetRotToTarget();
            if (Mathf.Abs(distance.x) < 0.05f && Mathf.Abs(distance.y) < 0.1f && Mathf.Abs(distance.z) < 0.05f && Mathf.Abs(rot) < 1)
            {
                stage = -1;
                staticEffect[0].gameObject.SetActive(true);
                Debug.Log("You Win");
                StartCoroutine(StartStageDelay(4, 5));
            }
        }

        if (stage == 4)
        {
            Vector3 distance = arm.GetComponent<CalcIK.CalcIK>().GetDistanceToTarget();
            float rot = arm.GetComponent<CalcIK.CalcIK>().GetRotToTarget();
            Debug.Log("DSad " + (dockingModule.transform.GetChild(0).position.z - GameObject.Find("TargetThingy").transform.position.z));
            if (dockingModule.transform.GetChild(0).position.z - GameObject.Find("TargetThingy").transform.position.z >= -0.3f & Stage4WinConditions())
            {
                stage = -1;
                staticEffect[0].gameObject.SetActive(true);
                Debug.Log("You Win");
                StartCoroutine(BackToScene());
            }
        }
    }

    public void SetupStage1()
    {
        stage = 1;
        screen.texture = renderers[0];
        staticEffect[0].gameObject.SetActive(false);
        staticEffect[1].gameObject.SetActive(false);
        dockingModule.SetActive(true);
        UI[0].SetActive(true);
        UI[1].SetActive(false);
        arm.SetActive(false);
        dockingModule.transform.position = dockingModule.transform.parent.position + new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-35, -55));
        dockingModule.transform.LookAt(dockingModule.transform.parent);
        dockingModule.transform.eulerAngles += new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20));
        dockingModule.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        crosshair.SetActive(true);
        dialogue.StartStageDialogue(1);
    }

    public void SetupStage2()
    {
        stage = 2;
        screen.texture = renderers[1];
        UI[0].SetActive(false);
        UI[1].SetActive(true);
        staticEffect[0].gameObject.SetActive(false);
        staticEffect[1].gameObject.SetActive(false);
        dockingModule.SetActive(false);
        arm.SetActive(true);
        arm.GetComponent<CalcIK.CalcIK>().Init();
        arm.GetComponent<CalcIK.CalcIK>().SetTarget(armTargets[0]);
        crosshair.SetActive(false);
        dialogue.StartStageDialogue(2);
    }

    public void SetupStage3()
    {
        stage = 3;
        screen.texture = renderers[1];
        UI[0].SetActive(false);
        UI[1].SetActive(true);
        staticEffect[0].gameObject.SetActive(false);
        staticEffect[1].gameObject.SetActive(false);
        dockingModule.SetActive(true);
        dockingModule.GetComponent<DockingModule>().enabled = false;
        dockingModule.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        dockingModule.transform.position = new Vector3(Random.Range(-6.67f, -3.79f), Random.Range(-2.14f, -1.09f), Random.Range(-107.7f, -106.45f));
        dockingModule.transform.eulerAngles = new Vector3(0, Random.Range(-10f, 10f), 0);
        arm.SetActive(true);
        GameObject[] arms = GameObject.FindGameObjectsWithTag("ArmIK");
        foreach(GameObject a in arms)
        {
            a.GetComponent<CalcIK.CalcIK>().Init();
            a.GetComponent<CalcIK.CalcIK>().SetTarget(armTargets[1]);
        }
        crosshair.SetActive(false);
        dialogue.StartStageDialogue(3);
    }

    public void SetupStage4()
    {
        stage = 4;
        screen.texture = renderers[0];
        UI[0].SetActive(false);
        UI[1].SetActive(true);
        staticEffect[0].gameObject.SetActive(false);
        staticEffect[1].gameObject.SetActive(false);
        dockingModule.transform.SetParent(attachToModule);
        armDockAttachers[0].GetComponent<Collider>().enabled = false;
        armDockAttachers[1].GetComponent<Collider>().enabled = false;
        GameObject[] arms = GameObject.FindGameObjectsWithTag("ArmIK");
        foreach (GameObject a in arms)
        {
            a.GetComponent<CalcIK.CalcIK>().IgnoreCollision();
        }
        crosshair.SetActive(true);
        dialogue.StartStageDialogue(4);
    }

    bool Stage1WinConditions()
    {
        Debug.Log((Mathf.Abs(dockingModule.transform.GetChild(0).position.z - dockingModule.transform.parent.position.z) > 0.2f) + " " +
            (Mathf.Abs(dockingModule.transform.GetChild(0).position.y - dockingModule.transform.parent.position.y) > 0.2f) + " " +
            (dockingModule.GetComponent<Rigidbody>().velocity.magnitude > 0.5) + " " +
            (Mathf.Abs(dockingModule.transform.eulerAngles.x)) + " " +
            (Mathf.Abs(dockingModule.transform.eulerAngles.y)) + " " +
            (Mathf.Abs(dockingModule.transform.eulerAngles.z)));

        dockingModule.transform.eulerAngles = new Vector3(dockingModule.transform.eulerAngles.x > 180 ? dockingModule.transform.eulerAngles.x - 360 : dockingModule.transform.eulerAngles.x,
            dockingModule.transform.eulerAngles.y > 180 ? dockingModule.transform.eulerAngles.y - 360 : dockingModule.transform.eulerAngles.y,
            dockingModule.transform.eulerAngles.z > 180 ? dockingModule.transform.eulerAngles.z - 360 : dockingModule.transform.eulerAngles.z);

        if (Mathf.Abs(dockingModule.transform.GetChild(0).position.z - dockingModule.transform.parent.position.z) > 0.2f) return false;
        if (Mathf.Abs(dockingModule.transform.GetChild(0).position.y - dockingModule.transform.parent.position.y) > 0.2f) return false;
        if (dockingModule.GetComponent<Rigidbody>().velocity.magnitude > 0.5) return false;
        if (Mathf.Abs(dockingModule.transform.eulerAngles.x) > 0.2f && Mathf.Abs(dockingModule.transform.eulerAngles.x) < 359.8f) return false;
        if (Mathf.Abs(dockingModule.transform.eulerAngles.y) > 0.2f && Mathf.Abs(dockingModule.transform.eulerAngles.y) < 359.8f) return false;
        if (Mathf.Abs(dockingModule.transform.eulerAngles.z) > 0.2f && Mathf.Abs(dockingModule.transform.eulerAngles.z) < 359.8f) return false;

        return true;
    }

    bool Stage4WinConditions()
    {

        Debug.Log((dockingModule.transform.GetChild(0).position.x - GameObject.Find("TargetThingy").transform.position.x) + " " + (dockingModule.transform.GetChild(0).position.y - GameObject.Find("TargetThingy").transform.position.y) + " " + (dockingModule.transform.eulerAngles.x) + " " + (dockingModule.transform.eulerAngles.y) + " " + (dockingModule.transform.eulerAngles.z));
        if (Mathf.Abs(dockingModule.transform.GetChild(0).position.x - GameObject.Find("TargetThingy").transform.position.x) > 0.2f) return false;
        if (Mathf.Abs(dockingModule.transform.GetChild(0).position.y - GameObject.Find("TargetThingy").transform.position.y) > 0.2f) return false;
        if (Mathf.Abs(dockingModule.transform.eulerAngles.y) > 0.5f && Mathf.Abs(dockingModule.transform.eulerAngles.y) < 359.5f) return false;

        return true;
    }

    IEnumerator StartStageDelay(int newStage, float delay)
    {
        yield return new WaitForSeconds(delay);
        stage = newStage;
        if (stage == 1) SetupStage1();
        if (stage == 2) SetupStage2();
        if (stage == 3) SetupStage3();
        if (stage == 4) SetupStage4();
    }

    IEnumerator BackToScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
