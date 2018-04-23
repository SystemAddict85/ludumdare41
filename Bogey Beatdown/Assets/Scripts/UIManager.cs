using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject HealthBar;
    public GameObject MessagePanel;

    private void Awake()
    {
        MessagePanel.SetActive(false);
    }

    public void UpdateHealthBar(float health, float max)
    {
        HealthBar.GetComponent<Image>().fillAmount = health / max;
    }

    public void ShowMessage(string message)
    {
        MessagePanel.SetActive(true);
        var tm = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        tm.text = message;
        tm.GetComponent<BlinkingText>().readyToBlink = true;
        tm.enabled = true;       
    }

    public void ShowMessage(string message, float delay)
    {
        MessagePanel.SetActive(true);
        var tm = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        tm.text = message;
        tm.GetComponent<BlinkingText>().readyToBlink = true;
        tm.enabled = true;
        Global.FreezeAllCharacters();
        StartCoroutine(HideMessage(2f));
    }

    IEnumerator HideMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        Global.UnfreezeAllCharacters();
        MessagePanel.SetActive(false);
    }
}
