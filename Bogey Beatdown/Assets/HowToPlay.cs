using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {
           
    public void StartTheGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
