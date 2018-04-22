using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{

    public float blinkingRate = 1f;
    private bool readyToBlink = true;

    private void Update()
    {
        if (readyToBlink)
        {
            readyToBlink = false;
            StartCoroutine(StartBlink());
        }

    }

    IEnumerator StartBlink()
    {
        GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        yield return new WaitForSeconds(blinkingRate / 2f);
        GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        yield return new WaitForSeconds(blinkingRate / 2f);
        readyToBlink = true;
    }


}
