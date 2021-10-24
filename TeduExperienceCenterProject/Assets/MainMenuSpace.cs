using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuSpace : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject beginPanel;
    [SerializeField] GameObject selectPanel;
    [SerializeField] GameObject startGamePanel;

    public void StartGame(int stage)
    {
        SpaceStationGameManager.stage = stage;
        beginPanel.SetActive(false);
        selectPanel.SetActive(false);
        startGamePanel.SetActive(true);
        StartCoroutine(DelayToStartGame());
    }

    IEnumerator DelayToStartGame()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}