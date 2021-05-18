using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text soulText;
    [SerializeField] private Text timeText;
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("ThemeMusic").GetComponent<MusicClass>().PlayMusic();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            soulText.text = "Most Souls: " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            soulText.text = "Most Souls: 0";
        }

        /*
        if (PlayerPrefs.HasKey("BestSec"))
        {
            timeText.text = "Best Time: " + PlayerPrefs.GetInt("BestMin") + " : " + PlayerPrefs.GetInt("BestSec");
        }
        else
        {
            timeText.text = "Best Time : NA";
        }
        */
    }
    public void LoadLevel(string level)
    {
        if (level == "Main")
        {
            GameObject.FindGameObjectWithTag("ThemeMusic").GetComponent<MusicClass>().StopMusic();
            Destroy(GameObject.FindGameObjectWithTag("ThemeMusic"));
        }
        Application.LoadLevel(level);
    }

    public void ClearHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        soulText.text = "Most Souls: 0";
    }
}
