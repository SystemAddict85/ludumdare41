using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{

    public float blinkingRate = 1f;
    [HideInInspector]
    public bool readyToBlink = true;

    private void Update()
    {
        if (readyToBlink)
        {
            readyToBlink = false;
            GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            StartCoroutine(StartBlink());
        }

    }

    IEnumerator StartBlink()
    {
        yield return new WaitForSeconds(blinkingRate / 2f);
        GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        yield return new WaitForSeconds(blinkingRate / 2f);
        GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        readyToBlink = true;
    }


}
