using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaypointsAndEnemyEvents : MonoBehaviour
{
    public int startingWaypointIndex;
    [SerializeField] AudioClip playOnWin;
    [SerializeField] MarsDialogue dialogue;
    public static int currentWaypointIndex = 0;
    float time;
    bool timing = false;
    Transform player;
    bool reachedPart5 = false;
    public static bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.init(2000);
        if(GameSettings.hoverbike) player = GameObject.Find("hover").transform;
        else player = GameObject.Find("Player").transform;

        currentWaypointIndex = startingWaypointIndex;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnd && timing)
        {
            time += Time.deltaTime;
            GameObject.Find("UI").GetComponent<UIUpdater>().UpdateTimeText(time);
        }
    }

    public void StartTiming()
    {
        timing = true;
    }

    public void ChangeWaypoint()
    {
        transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(false);
        if (currentWaypointIndex != 8) currentWaypointIndex += 1;
        WaypointPassed(currentWaypointIndex);
       
    }

    public void WaypointPassed(int point)
    {
        if(point == 1)
        {
            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if(point == 2)
        {
            for(int i = 0; i < 2; i++)
            {
                SpawnDroneBall(transform.GetChild(0).GetChild(0).position + (Vector3.forward * 10 * i), 35, 2f);
            }

            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if (point == 3)
        {
            StartCoroutine(Crater1());
        }
        else if (point == 4)
        {
            StartCoroutine(Crater2());
            StartCoroutine(Crater2pt2());
            StartCoroutine(Crater2pt3());
            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if (point == 5)
        {
            StartCoroutine(Crater3());
            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if (point == 6)
        {
            StartCoroutine(Valley());
            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if (point == 7)
        {
            transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        }
        else if (point == 8)
        {
            gameEnd = true;
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            for(int i = 0; i < 5; i++)
            {
                transform.GetChild(transform.childCount - 1).GetChild(i).GetComponent<Turret>().StartShooting();
            }
            StartCoroutine(GameEnd());
        }

        dialogue.StartWaypointDialogue(point);
    }

    IEnumerator Crater1()
    {
        Debug.Log("1 start");
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                int radius = Random.Range(75, 125);
                float angle = Random.Range(player.eulerAngles.y - 90f, player.eulerAngles.y + 90f) - 90;

                if(j == 0) SpawnDroneBall(transform.GetChild(3).position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);
                if (j == 1) SpawnDroneNormal(transform.GetChild(3).position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);
                if (j == 2) SpawnDroneInsect(transform.GetChild(3).position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);

            }
            yield return new WaitForSeconds(5);
        }

        transform.GetChild(currentWaypointIndex + 1).gameObject.SetActive(true);
        Debug.Log("1 end");
    }

    IEnumerator Crater2()
    {
        Debug.Log("2 start");
        while (!reachedPart5)
        {
            for (int j = 0; j < 3; j++)
            {
                int radius = Random.Range(75, 125);
                float angle = Random.Range(player.eulerAngles.y - 90f, player.eulerAngles.y + 90f) - 90;

                if (j == 0) SpawnDroneBall(player.position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);
                if (j == 1) SpawnDroneNormal(player.position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);
                if (j == 2) SpawnDroneInsect(player.position + new Vector3(Mathf.Sin(angle) * radius, 250, Mathf.Cos(angle) * radius), -225, 5f);

                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator Crater2pt2()
    {
        Debug.Log("2.1 start");
        yield return new WaitForSeconds(15);
        Debug.Log("2.1 end");
        SpawnDroneZR7(transform.GetChild(0).GetChild(2).position + Vector3.up * 225, -225, 10f);
    }

    IEnumerator Crater2pt3()
    {
        Debug.Log("2.2 start");
        while (!reachedPart5)
        {
            SpawnDroneQuad(transform.GetChild(0).GetChild(1).position, 50, 5f);
            yield return new WaitForSeconds(40);
        }
    }

    IEnumerator Crater3()
    {
        Debug.Log("3 start");
        SpawnDroneZR7(transform.GetChild(0).GetChild(3).position + Vector3.up * 225, -225, 10f);
        while (!reachedPart5)
        {
            SpawnDroneQuad(transform.GetChild(0).GetChild(4).position, 50, 5f);
            yield return new WaitForSeconds(40);
        }
    }

    IEnumerator Valley()
    {
        Debug.Log("4 start");
        SpawnDroneZR7(transform.GetChild(0).GetChild(5).position + Vector3.up * 225, -225, 10f);
        while (!gameEnd)
        {
            SpawnDroneBall(transform.GetChild(0).GetChild(5).position + new Vector3(Random.Range(-20,20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            SpawnDroneNormal(transform.GetChild(0).GetChild(2).position + new Vector3(Random.Range(-20, 20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            SpawnDroneInsect(transform.GetChild(0).GetChild(5).position + new Vector3(Random.Range(-20, 20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            yield return new WaitForSeconds(8);
            SpawnDroneBall(transform.GetChild(0).GetChild(2).position + new Vector3(Random.Range(-20, 20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            SpawnDroneNormal(transform.GetChild(0).GetChild(5).position + new Vector3(Random.Range(-20, 20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            SpawnDroneInsect(transform.GetChild(0).GetChild(2).position + new Vector3(Random.Range(-20, 20), Random.Range(240, 260), Random.Range(-20, 20)), -225, 5f);
            yield return new WaitForSeconds(8);
        }
    }

    IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(10);

        while (GameObject.Find("GameEndImage").GetComponent<Image>().color.a < 1f)
        {
            GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, GameObject.Find("GameEndImage").GetComponent<Image>().color.a + (Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(0);
    }

    void SpawnDroneBall(Vector3 position, float vertMoveDist, float vertMoveTime)
    {
        if (gameEnd) return;
        GameObject droneball = Instantiate(Resources.Load<GameObject>("Drone_Ball"), position, transform.GetChild(0).GetChild(0).rotation);
        droneball.GetComponent<Drone_Ball>().verticalMovementDistance = vertMoveDist;
        droneball.GetComponent<Drone_Ball>().verticalMovementTime = vertMoveTime;
        droneball.GetComponent<Drone_Ball>().Init();
    }

    void SpawnDroneNormal(Vector3 position, float vertMoveDist, float vertMoveTime)
    {
        if (gameEnd) return;
        GameObject dronenormal = Instantiate(Resources.Load<GameObject>("Drone_Normal"), position, transform.GetChild(0).GetChild(0).rotation);
        dronenormal.GetComponent<Drone_Normal>().verticalMovementDistance = vertMoveDist;
        dronenormal.GetComponent<Drone_Normal>().verticalMovementTime = vertMoveTime;
        dronenormal.GetComponent<Drone_Normal>().Init();
    }

    void SpawnDroneInsect(Vector3 position, float vertMoveDist, float vertMoveTime)
    {
        if (gameEnd) return;
        GameObject droneinsect = Instantiate(Resources.Load<GameObject>("Drone_Insect"), position, transform.GetChild(0).GetChild(0).rotation);
        droneinsect.GetComponent<Drone_Insect>().verticalMovementDistance = vertMoveDist;
        droneinsect.GetComponent<Drone_Insect>().verticalMovementTime = vertMoveTime;
        droneinsect.GetComponent<Drone_Insect>().Init();
    }

    void SpawnDroneQuad(Vector3 position, float vertMoveDist, float vertMoveTime)
    {
        if (gameEnd) return;
        GameObject dronequad = Instantiate(Resources.Load<GameObject>("Drone_Quad"), position, transform.GetChild(0).GetChild(0).rotation);
        dronequad.GetComponent<Drone_Quad>().verticalMovementDistance = vertMoveDist;
        dronequad.GetComponent<Drone_Quad>().verticalMovementTime = vertMoveTime;
        dronequad.GetComponent<Drone_Quad>().Init();
    }

    void SpawnDroneZR7(Vector3 position, float vertMoveDist, float vertMoveTime)
    {
        if (gameEnd) return;
        GameObject dronezr7 = Instantiate(Resources.Load<GameObject>("Drone_ZR7"), position, transform.GetChild(0).GetChild(0).rotation);
        dronezr7.GetComponent<Drone_ZR7>().verticalMovementDistance = vertMoveDist;
        dronezr7.GetComponent<Drone_ZR7>().verticalMovementTime = vertMoveTime;
        dronezr7.GetComponent<Drone_ZR7>().Init();
    }
}
