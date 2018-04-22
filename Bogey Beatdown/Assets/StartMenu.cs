using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(.5f);
        StartScene();
    }

    void StartScene()
    {
        AudioManager.ChangeMusic(AudioManager.Instance.audioList.peacefulMusic);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
