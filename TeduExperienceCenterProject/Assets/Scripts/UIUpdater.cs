using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] Text pointsText;
    [SerializeField] Text timeText;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject altitudeBar;

    int points;

    public void UpdatePointsText(int points)
    {
        this.points += points;
        pointsText.text = "" + this.points;
    }

    public void UpdateTimeText(float time)
    {

        float seconds = time % 60;
        int minutes = (int)(time / 60);

        if(minutes != 0)
        {
            string timeStr = "" + seconds;
            string[] timeFragments = timeStr.Split('.');
            if(seconds < 10)
            {
                timeText.text = minutes + ":0" + timeFragments[0] + "." + timeFragments[1][0];
            }
            else
            {
                timeText.text = minutes + ":" + timeFragments[0] + "." + timeFragments[1][0];
            }
            
        }
        else
        {
            string timeStr = "" + time;
            string[] timeFragments = timeStr.Split('.');
            timeText.text = timeFragments[0] + "." + timeFragments[1][0];
        }
    }

    public void UpdateHealthBar(float health)
    {
        healthBar.GetComponent<Slider>().value = health;
        Color a = Color.HSVToRGB(health / 534f, 61 / 100f, 1f);
        healthBar.transform.GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.06f);
        healthBar.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.59f);
        healthBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 1);
        healthBar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + health;
    }

    public void UpdateAltitudeBar(float altitude)
    {
        int alt = (int)(altitude * 10);
        altitudeBar.GetComponent<Slider>().value = Mathf.Min(alt / 10f, 50);
        Color a = Color.HSVToRGB((50 - Mathf.Min(altitude, 50)) / 132.178f, 61 / 100f, 1f);
        altitudeBar.transform.GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.06f);
        altitudeBar.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 0.59f);
        altitudeBar.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(a.r, a.g, a.b, 1);
        altitudeBar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + (alt / 10f);
    }
}
