using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject beginPanel;
    [SerializeField] GameObject startGamePanel;

    public void StartGame(bool hoverbike)
    {
        GameSettings.hoverbike = hoverbike;
        beginPanel.SetActive(false);
        startGamePanel.SetActive(true);
        StartCoroutine(DelayToStartGame());
    }

    IEnumerator DelayToStartGame()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
