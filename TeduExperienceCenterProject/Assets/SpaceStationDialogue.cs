using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceStationDialogue : MonoBehaviour
{
    [SerializeField] SpaceStationGameManager gameManager;
    [SerializeField] GameObject initCamera;
    [SerializeField] GameObject camPathObject;
    [SerializeField] CPC_CameraPath camPath;
    [SerializeField] Text dialogueText;
    [SerializeField] GameObject[] joystickArrows;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (SpaceStationGameManager.stage == 0) StartCoroutine(BeforeStartGame());

    }

    // Update is called once per frame
    void Update()
    {
        joystickArrows[0].transform.localEulerAngles += Vector3.up * Time.deltaTime * 15;
        joystickArrows[1].transform.localEulerAngles += Vector3.forward * Time.deltaTime * 15;
        joystickArrows[2].transform.localEulerAngles += Vector3.forward * Time.deltaTime * 15;
    }

    public void StartStageDialogue(int stage)
    {
        StopAllCoroutines();
        if (stage == 1) StartCoroutine(Stage1Dialogue());
        if (stage == 2) StartCoroutine(Stage2Dialogue());
        if (stage == 3) StartCoroutine(Stage3Dialogue());
        if (stage == 4) StartCoroutine(Stage4Dialogue());
    }

    IEnumerator BeforeStartGame()
    {
        camPath.PlayPath(16);
        dialogueText.text = "Welcome future astronaut! Today you will learn how to dock a space module and control a space arm.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-1-1"));
        yield return new WaitForSeconds(7);
        dialogueText.text = "The space arm is very useful; it can be used to dock a module, repair certain parts of the ship and guide astronauts on the station.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-1-2"));
        yield return new WaitForSeconds(9);
        initCamera.transform.localPosition = Vector3.zero;
        initCamera.transform.localEulerAngles = Vector3.zero;
        gameManager.SetupStage1();
    }

    IEnumerator Stage1Dialogue()
    {
        dialogueText.text = "In this mission, you need to dock the module onto the space station.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-2-1"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "On the screen, you can see how far it is from the target.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-2-2"));
        yield return new WaitForSeconds(5);
        dialogueText.text = "Using the joysick and thumbstick, you want to orient the module so that all the values on screen are at 0.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-2-3"));
        yield return new WaitForSeconds(7);
        dialogueText.text = "Moving the joystick will rotate the module, and the thumbstick will change the position.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-2-4"));
        yield return new WaitForSeconds(5);
        dialogueText.text = "Click the thumbstick in any direction and the module will speed up in that direction.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-2-5"));
        yield return new WaitForSeconds(6);
        while (true)
        {
            dialogueText.text = "Using the joysick and thumbstick, you want to orient the module so that all the values on screen are at 0.";
            yield return new WaitForSeconds(7);
            dialogueText.text = "Moving the joystick will rotate the module, and the thumbstick will change the position.";
            yield return new WaitForSeconds(5);
            dialogueText.text = "Click the thumbstick in any direction and the module will speed up in that direction.";
            yield return new WaitForSeconds(6);
        }
        
    }

    IEnumerator Stage2Dialogue()
    {
        joystickArrows[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Yaw";
        joystickArrows[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Arm Base Rotation";
        joystickArrows[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Pitch";
        joystickArrows[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Z";
        joystickArrows[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X";
        dialogueText.text = "Now you will learn how to control the arm.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-1"));
        yield return new WaitForSeconds(5);
        dialogueText.text = "The space station has an arm with many docking points, so it can crawl across the space station.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-2"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-3"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Like the last mission, use the joysick to rotate, and the thumbstick to move.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-4"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Move and rotate the arm to make values on screen 0.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-5"));
        yield return new WaitForSeconds(6);
        while (true)
        {
            dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Like the last mission, use the joysick to rotate, and the thumbstick to move.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Move and rotate the arm to make values on screen 0.";
            yield return new WaitForSeconds(6);
        }
    }

    IEnumerator Stage3Dialogue()
    {
        joystickArrows[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Yaw";
        joystickArrows[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Arm Base Rotation";
        joystickArrows[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Pitch";
        joystickArrows[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Z";
        joystickArrows[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X";
        dialogueText.text = "Now we will use the arm to catch a module in space.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-4-1"));
        yield return new WaitForSeconds(5);
        dialogueText.text = "Use the arm to find the module, and dock onto its docking point.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-4-2"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-3"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Use the joysick to rotate, and the thumbstick to move.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-4"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Move and rotate the arm to make the numbers in green 0.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-5"));
        yield return new WaitForSeconds(6);
        while (true)
        {
            dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Use the joysick to rotate, and the thumbstick to move.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Move and rotate the arm to make the numbers in green 0.";
            yield return new WaitForSeconds(6);
        }
    }

    IEnumerator Stage4Dialogue()
    {
        joystickArrows[0].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Yaw";
        joystickArrows[1].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Arm Base Rotation";
        joystickArrows[2].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Head Pitch";
        joystickArrows[3].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Z";
        joystickArrows[4].transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "X";
        dialogueText.text = "Now that we have caught our module, lets dock it";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-5-1"));
        yield return new WaitForSeconds(5);
        dialogueText.text = "Use the arm controls to align the module with the docking port, and dock the module.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-5-2"));
        yield return new WaitForSeconds(7);
        dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-3"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Use the joysick to rotate, and the thumbstick to move.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-4"));
        yield return new WaitForSeconds(6);
        dialogueText.text = "Move and rotate the arm to make the numbers in green 0.";
        source.PlayOneShot(Resources.Load<AudioClip>("Audio/Space-3-5"));
        yield return new WaitForSeconds(6);
        while (true)
        {
            dialogueText.text = "You must move and rotate the arm to dock on the docking point.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Use the joysick to rotate, and the thumbstick to move.";
            yield return new WaitForSeconds(6);
            dialogueText.text = "Move and rotate the arm to make the numbers in green 0.";
            yield return new WaitForSeconds(6);
        }
    }
}
