using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

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
        //AudioManager.ChangeMusic(AudioManager.Instance.audioList.peacefulMusic);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("how_to_play");
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
