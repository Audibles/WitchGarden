using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text myScore;
    public int score;


    void Start()
    {

        myScore = GetComponent<Text>() as Text;
        score = 0;
        //startTime = Time.time + 180;

    }

    void Update()
    {
        //score = player.score;
        myScore.text = string.Format("Score: ${0}", score);

    }
}
