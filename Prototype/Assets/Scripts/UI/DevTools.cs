using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DevTools : MonoBehaviour
{
    public static DevTools instance;
    [SerializeField] GameObject stats;
    [SerializeField] GameObject tools;
    private int objCount;
    [SerializeField] private Text noOfObjects;
    private float timer = 0.0f;
    [SerializeField] private Text timePlayed;
    public int minutes;
    public int seconds;
    public bool statsShown;
    public bool toolsShown;



    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        statsShown = false;
        toolsShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        objCount = GameObject.FindGameObjectsWithTag("Player").Length;
        objCount += GameObject.FindGameObjectsWithTag("Enemy").Length;
        objCount += GameObject.FindGameObjectsWithTag("Soul").Length;
        objCount += GameObject.FindGameObjectsWithTag("SoulGem").Length;
        noOfObjects.text = "No.of objects: " + objCount;

        timer += Time.deltaTime;
        minutes = (int)(timer / 60);
        seconds = (int)(timer % 60);
        timePlayed.text = "Time Played: " + minutes + ":" + seconds; 

        if (Input.GetKeyDown("k"))
        {
            statsShown = !statsShown;
        }
        if (Input.GetKeyDown("l"))
        {
            toolsShown = !toolsShown;
        }
        if (toolsShown)
        {
            tools.gameObject.SetActive(true);
        }
        else
        {
            tools.gameObject.SetActive(false);
        }

        if (statsShown)
        {
            stats.gameObject.SetActive(true);
        }
        else
        {
            stats.gameObject.SetActive(false);
        }
    }
}
