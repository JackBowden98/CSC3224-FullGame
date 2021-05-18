using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text soulText;
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
