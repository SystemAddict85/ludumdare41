using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {


    private void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(.5f);
        StartScene();
    }

    void StartScene()
    {
      //  AudioManager.ChangeMusic(AudioManager.Instance.audioList.gameOverMusic);
    }

    public void BackToStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("start_menu");
    }
}
