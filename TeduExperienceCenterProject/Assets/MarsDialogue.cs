using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarsDialogue : MonoBehaviour
{
    [SerializeField] WaypointsAndEnemyEvents manager;
    [SerializeField] Text dialogueText;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (manager.startingWaypointIndex == 0) StartCoroutine(BeforeStartGame());
    }

    public void StartWaypointDialogue(int waypoint)
    {
        StopAllCoroutines();
        if (waypoint == 1) StartCoroutine(Stage1Dialogue());
        if (waypoint == 2) StartCoroutine(Stage2Dialogue());
        if (waypoint == 3) StartCoroutine(Stage3Dialogue());
        if (waypoint == 4) StartCoroutine(Stage4Dialogue());
        if (waypoint == 5) StartCoroutine(Stage5Dialogue());
        if (waypoint == 6) StartCoroutine(Stage6Dialogue());
        if (waypoint == 7) StartCoroutine(Stage7Dialogue());
        if (waypoint == 8) StartCoroutine(Stage8Dialogue());
    }

    IEnumerator BeforeStartGame()
    {
        dialogueText.text = "Welcome to the chinese mars station, established in 2030.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-1-1"));
        yield return new WaitForSeconds(4);
        dialogueText.text = "There have been reports of some mysterous objects that have close to the station in the past 24 hours.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-1-2"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "We need you to go and investigate the landing sites to see what's there. We have prepared a high-tech vehicle to help you.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-1-3"));
        yield return new WaitForSeconds(7);
        dialogueText.text = "Use your joystick to control the vehicle.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-1-4"));
        yield return new WaitForSeconds(7);
    }

    IEnumerator Stage1Dialogue()
    {
        dialogueText.text = "I can see the crash site! Just keep going forward.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-2-1"));
        yield return new WaitForSeconds(4);
    }

    IEnumerator Stage2Dialogue()
    {
        dialogueText.text = "There are reports of drone activity to your left.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-3-1"));
        yield return new WaitForSeconds(3);
        dialogueText.text = "There they are! Click the trigger to shoot a carrot to destroy the drone!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-3-2"));
        yield return new WaitForSeconds(2);
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0) 
        {
            yield return new WaitForEndOfFrame();
        }
        dialogueText.text = "Great job! Now head to the crash site.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-3-3"));
    }

    IEnumerator Stage3Dialogue()
    {
        dialogueText.text = "Interesting. Looks like some power cells.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-4-1"));
        yield return new WaitForSeconds(2);
        dialogueText.text = "More Drones! Attack them while I investigate this object!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-4-2"));
        yield return new WaitForSeconds(30);
        dialogueText.text = "These are high-power nuclear power cells from earth, and we need these to power the station. Get to the other sites!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-4-3"));
    }

    IEnumerator Stage4Dialogue()
    {
        dialogueText.text = "Good job! Now get to the last one!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-5-1"));
        yield return new WaitForSeconds(3);
        dialogueText.text = "Oh no! There are a lot more drones! Let's hurry up!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-5-2"));
        yield return new WaitForSeconds(15);
        dialogueText.text = "A big drone has appeared and will shoot a big laser at you, so make sure to keep moving.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-5-3"));
    }

    IEnumerator Stage5Dialogue()
    {
        dialogueText.text = "Nice job! Now hurry back to base! Go through the canyon!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-6-1"));
        yield return new WaitForSeconds(1);
    }

    IEnumerator Stage6Dialogue()
    {
        dialogueText.text = "There are a lot of enemies on the side, get rid of them!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-7-1"));
        yield return new WaitForSeconds(1);
    }

    IEnumerator Stage7Dialogue()
    {
        dialogueText.text = "I can see the base! Get there and our turrets will handle everything else!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-8-1"));
        yield return new WaitForSeconds(1);
    }

    IEnumerator Stage8Dialogue()
    {
        dialogueText.text = "The turrets have been powered with the batteries that we found!";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-9-1"));
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForEndOfFrame();
        }
        dialogueText.text = "Great job soldier! We couldn't have done it without you.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Mars-9-2"));
    }
}
