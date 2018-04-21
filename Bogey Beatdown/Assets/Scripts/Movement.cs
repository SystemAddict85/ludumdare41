using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeedHorizontal = 1f;
    public float moveSpeedVertical = .75f;
    public float diagonalSlow = .75f;

    public float currentScaleX = 1f;

    public void Move(float x, float y)
    {
        var stepX = x * Time.deltaTime * moveSpeedHorizontal;
        var stepY = y * Time.deltaTime * moveSpeedVertical;

        if (Mathf.Abs(x) > 0 && Mathf.Abs(y) > 0)
        {
            stepX *= diagonalSlow;
            stepY *= diagonalSlow;
        }

        transform.position += new Vector3(stepX, stepY);

        if (stepX < 0)        
            currentScaleX = -1;
        else if (stepX > 0)
            currentScaleX = 1;

        transform.localScale = new Vector3(currentScaleX, 1, 1);

    }


}
