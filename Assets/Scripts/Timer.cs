using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public Text myTimer;
    private float startTime;
    private float guiTime;
    public float setTime;


    void Start()
    {
        setTime = 360;
        myTimer = GetComponent<Text>() as Text;
        //startTime = 10;
        startTime = Time.time + setTime;

    }

    void Update()
    {
        guiTime = startTime - Time.time;
        float minutes = guiTime / 60;
        float seconds = guiTime % 60;

        //float fraction = (time * 100) % 100;
        //update the label value
        myTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);

        if (guiTime < 0) {
            SceneManager.LoadScene("MenuScene");
        } 

    }
}
