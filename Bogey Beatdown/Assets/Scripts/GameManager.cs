using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { private set; get; }

    public Character Player;
    public GolfBall GolfBall;
    public UIManager UI;

	void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        GolfBall = GameObject.FindObjectOfType<GolfBall>();
        UI = GameObject.Find("GUI").GetComponent<UIManager>();
    }

    public void LaunchStage(int stageNumber)
    {
        
    }

    public void GameOver(string message)
    {
        Global.FreezeAllCharacters();
        UI.ShowMessage(message);
        StartCoroutine(DelaySceneChange());
    }
    IEnumerator DelaySceneChange() {

        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("game_over");
    }

    

}
