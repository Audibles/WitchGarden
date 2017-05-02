using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text myScore;
    int score;
	public GameObject playerObj;
	PlayerMovement player;

    void Start()
    {
		this.player = (PlayerMovement)playerObj.GetComponent(typeof(PlayerMovement));
        myScore = GetComponent<Text>() as Text;
        this.score = 0;
        //startTime = Time.time + 180;

    }

    void Update()
    {
        this.score = player.score;
        myScore.text = string.Format("Score: {0}", score);
		Debug.Log (score);
    }
}
