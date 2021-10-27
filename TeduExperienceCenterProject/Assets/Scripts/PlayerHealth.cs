using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] AudioClip playOnDeath;
    [SerializeField] bool hoverbike;

    float health;
    public static bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        GameObject.Find("UI").GetComponent<UIUpdater>().UpdateHealthBar(health);
        if(!hoverbike && GameSettings.hoverbike)
        {
            gameObject.tag = "Untagged";
            Destroy(this);
        }
        if (hoverbike && !GameSettings.hoverbike)
        {
            gameObject.tag = "Untagged";
            Destroy(this);
        }
    }

    private void Update()
    {
        //Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical") + " " + Input.GetAxis("Axis3") + " " + Input.GetAxis("Axis4") + " " + Input.GetAxis("Axis5") + " " + Input.GetAxis("Axis6"));
        //Debug.Log(Input.GetButton("Fire1") + " " + Input.GetButton("Fire2") + " " + Input.GetButton("Fire3") + " " + Input.GetButton("JB3") + " " + Input.GetButton("JB4") + " " + Input.GetButton("JB5"));
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("HP: " + health);
        if(!WaypointsAndEnemyEvents.gameEnd) GameObject.Find("UI").GetComponent<UIUpdater>().UpdateHealthBar(health);

        if (health <= 0 && !dead)
        {
            dead = true;
            Debug.Log("death");
            StartCoroutine(OnDeath());
        }
    }

    IEnumerator OnDeath()
    {
        Debug.Log("dos");
        if (GameSettings.hoverbike) GameObject.Find("hover").GetComponent<Hoverbike>().enabled = false;
        else GameObject.Find("Player").GetComponent<Controller_Rover>().enabled = false;

        //GameObject.Find("CamContainer").GetComponent<KeepRotation>().enabled = false;
        GetComponent<AudioSource>().PlayOneShot(playOnDeath);

        while (GameObject.Find("GameEndImage").GetComponent<Image>().color.a < 0.25f)
        {
            GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(0.5f, 0f, 0f, GameObject.Find("GameEndImage").GetComponent<Image>().color.a + (Time.deltaTime / 2));
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(0.5f, 0f, 0f, 0.25f);

        yield return new WaitForSeconds(3);

        while (GameObject.Find("GameEndImage").GetComponent<Image>().color.a < 1f)
        {
            GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(GameObject.Find("GameEndImage").GetComponent<Image>().color.r - (Time.deltaTime / 3), 0f, 0f, GameObject.Find("GameEndImage").GetComponent<Image>().color.a + (Time.deltaTime / 2));
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("GameEndImage").GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);

        SceneManager.LoadScene(0);
    }
}
