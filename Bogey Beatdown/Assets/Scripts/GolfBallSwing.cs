using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfBallSwing : MonoBehaviour {

    private bool isPlayerInRange = false;
    private GolfBall golfBall;


    private void Awake()
    {
        golfBall = GetComponentInParent<GolfBall>();
    }

    private void Update()
    {
        if (!GameManager.Instance.CurrentStage)
        {
            GetComponent<Collider2D>().enabled = true;
            if (isPlayerInRange)
                CheckForPlayerInteraction();
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = true;
            golfBall.ui.StartGameButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = false;
            golfBall.ui.StartGameButton.SetActive(false);
        }
    }
    
    private void CheckForPlayerInteraction()
    {
        if (Input.GetButtonDown("Attack") && golfBall.canPlayGolf)
        {
            golfBall.canPlayGolf = false;
            golfBall.StartBallGame();
        }
        
    }
    
}
