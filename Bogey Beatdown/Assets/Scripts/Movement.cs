using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeedHorizontal = 1f;
    public float moveSpeedVertical = .75f;
    public float diagonalSlow = .75f;

    [HideInInspector]
    public float currentScaleX = 1f;

    [HideInInspector]
    public bool canMove = true;

    public void Move(float x, float y)
    {
        if (canMove)
        {
            var stepX = x * Time.deltaTime * moveSpeedHorizontal;
            var stepY = y * Time.deltaTime * moveSpeedVertical;

            // check if character's movespeed affects the movement
            var stats = GetComponent<CharacterStats>();
            if (stats)
            {
                stepX *= stats.moveSpeed;
                stepY *= stats.moveSpeed;
            }

            if (Mathf.Abs(x) > 0 && Mathf.Abs(y) > 0)
            {
                stepX *= diagonalSlow;
                stepY *= diagonalSlow;
            }

            transform.position += new Vector3(stepX, stepY);

            var anim = GetComponent<Animator>();
            if (anim && (Mathf.Abs(stepX) > 0 || Mathf.Abs(stepY) > 0)) {
                anim.SetBool("isWalking", true);
            } else
            {
                anim.SetBool("isWalking", false);
            }

            if (stepX < 0)
                currentScaleX = -1;
            else if (stepX > 0)
                currentScaleX = 1;

            transform.localScale = new Vector3(currentScaleX, 1, 1);
        }
    }

    public void ToggleMove(bool enabled)
    {
        canMove = enabled;
    }
}
