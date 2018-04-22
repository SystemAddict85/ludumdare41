using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour {


    public float pollingDelay = .4f;
    private bool readyToPoll = true;
    
	// Update is called once per frame
	void Update () {

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
}
