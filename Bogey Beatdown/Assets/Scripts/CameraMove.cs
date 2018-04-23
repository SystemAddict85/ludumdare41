using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool isPlayerInRange;

    private Movement move;
    public float maxDistFromBall = 2f;

    void Awake()
    {
        move = GetComponentInParent<Movement>();
    }

    void Update()
    {
        if (isPlayerInRange 
            && (GetDistanceFromBall() <= maxDistFromBall 
            || transform.position.x < GameManager.Instance.GolfBall.transform.position.x))
        {
            CheckForCameraMove();
        }
    }

    void CheckForCameraMove()
    {
        var x = Input.GetAxisRaw("Horizontal");

        if(x > 0)
            move.MoveCamera(x, 0, GameManager.Instance.Player.stats.moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }

    float GetDistanceFromBall()
    {
        return Vector2.Distance(transform.position, GameManager.Instance.GolfBall.transform.position);
    }

}
