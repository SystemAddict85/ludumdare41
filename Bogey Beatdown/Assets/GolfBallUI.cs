using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBallUI : MonoBehaviour {

    [HideInInspector]
    public Canvas canvas;
    public GameObject StartGameButton;
    public GameObject GolfGameGauge;
    [HideInInspector]
    public GolfGameCursor gaugeCursor;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        StartGameButton.SetActive(false);
        gaugeCursor = GolfGameGauge.GetComponentInChildren<GolfGameCursor>();
        GolfGameGauge.SetActive(false);
    }
}
