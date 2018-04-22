using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { private set; get; }

    public Character Player;

	void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    public static void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game_over");
    }

    

}
