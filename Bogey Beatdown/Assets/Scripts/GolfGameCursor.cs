using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfGameCursor : MonoBehaviour
{

    private GolfBall ball;

    public float moveRate = 2.5f;

    private int dir = 1;
    private bool isMoving = false;

    private void Awake()
    {
        ball = GetComponentInParent<GolfBall>();
    }
    void Update()
    {
        if (isMoving)
        {
            MoveCursor();
        }
    }    

    private void MoveCursor()
    {
        var rect = GetComponent<RectTransform>();

        if (Input.GetButtonDown("GolfSwing") || Input.GetButtonDown("Attack"))
        {
            HandleInput(Mathf.Abs(rect.anchoredPosition.x));
            return;
        }

        var step = moveRate * dir * Time.deltaTime;
        if (Mathf.Abs(step + rect.anchoredPosition.x) > 1f)
        {
            dir *= -1;
            rect.anchoredPosition = new Vector2(-dir, 0);
        }
        else
        {
            rect.anchoredPosition = new Vector2(step + rect.anchoredPosition.x, 0);
        }

    }

    private void HandleInput(float x)
    {
        isMoving = false;
        var power = GetLaunchPower(x);
        if (power == 0)
        {
            ball.Fail();
        }
        else
        {
            ball.StartLaunch(power);
        }
    }

    private float GetLaunchPower(float x)
    {
        if (x < .3)
        {
            // good hit
            return 2;
        }
        else if (x < .6)
        {
            // okay hit
            return 1.5f;
        }
        else if (x < .8)
        {
            // passing hit
            return 1;
        }
        else
        {
            // failed
            return 0;
        }

    }

    public void StartCursor()
    {
        var rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-1f, 0f);
        dir = 1;
        isMoving = true;
    }


}
