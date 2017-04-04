using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadOnClick : MonoBehaviour {

    GameObject UI_Canvas;
    GameObject Title_Screen;

    // Use this for initialization
    void Start () {
        Scene currentScene = SceneManager.GetActiveScene();
        string currentSceneName = currentScene.name;
        if (currentSceneName == "MainScene")
        {
            SceneManager.LoadScene("MenuScene");
            SceneManager.UnloadSceneAsync("MainScene");
        }
        UI_Canvas = GameObject.Find("UI_Canvas");
        Title_Screen = GameObject.Find("Title Screen");
        DontDestroyOnLoad(UI_Canvas);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene() {
        //Title_Screen.SetActive(false);
        SceneManager.LoadScene("MainScene");
        //UIManager uimanager = UIManager.uiManager;
        //print(uimanager);
        //uimanager.LoadScene();
    }
}
