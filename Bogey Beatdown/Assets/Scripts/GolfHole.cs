using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "GolfBall")
        {            
            Debug.Log("You win!!!");
            GameManager.Instance.GameOver("You win");
        }
    }
}
