using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public static UIManager uiManager;

    GameObject LoadingBackground;
    GameObject Score;

    // Use this for initialization
    void Start () {
        if (uiManager == null){
            uiManager = this;
            print("here");
        }
        else {
            Destroy(gameObject);
            print("Destroyed");
        }
        //LoadingBackground = GameObject.Find("Loading_Background");
        //Score = GameObject.Find("Score");
        LoadingBackground.SetActive(false);
    }

    public void TakeLife(){
        SceneManager.LoadScene("MenuScene");
        }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
