using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{

    public static SpriteManager Instance { get; private set; }

    public float pollingDelay = .4f;
    private bool readyToPoll = true;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }
    // Update is called once per frame
    void Update()
    {

        if (readyToPoll)
            AdjustSortingLayers();

    }

    private void AdjustSortingLayers()
    {
        readyToPoll = false;

        var chars = GameObject.FindObjectsOfType<SortedSprite>();
        var sorted = new List<SortedSprite>(chars.OrderByDescending(x => x.transform.position.y));

        for (int i = 0; i < sorted.Count; i++)
            sorted[i].rend.sortingOrder = i;

        StartCoroutine(PollDelay());
    }

    private IEnumerator PollDelay()
    {
        yield return new WaitForSeconds(pollingDelay);
        readyToPoll = true;
    }

    public static void BlinkSprite(SpriteRenderer rend, float totalDuration)
    {
        Instance.StartCoroutine(Blink(rend, totalDuration));
    }

    static IEnumerator Blink(SpriteRenderer rend, float duration)
    {
        var rateOfBlink = .1f;
        var currentTime = 0f;
        while (currentTime + rateOfBlink < duration)
        {
            if (rend)
                rend.enabled = false;
            yield return new WaitForSeconds(rateOfBlink / 2);
            if (rend)
                rend.enabled = true;
            yield return new WaitForSeconds(rateOfBlink / 2);
            currentTime += rateOfBlink;
        }
        if(rend)
            rend.enabled = true;
    }
}
