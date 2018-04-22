using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public enum AI { IDLE, PATROL, APPROACH, ATTACK };
    
    public AI State { private set; get; }

    private Movement move;
    private Character chara;

    private int moveDirection = 1;
    private bool readyToLook = true;

    public float sightRange = 1f;
    public float approachDistance = 1f;



    private void Awake()
    {
        move = GetComponent<Movement>();
        State = AI.IDLE;
        chara = GetComponent<Character>();
    }

    private void Start()
    {
        StartPatrol();
    }

    private void Update()
    {
        MakeDecision();
    }

    void MakeDecision()
    {
        switch (State)
        {
            case AI.IDLE:
                break;
            case AI.PATROL:
                Patrol();
                break;
            case AI.APPROACH:
                StartApproach();
                break;
            case AI.ATTACK:
                break;
        }
    }

    void Patrol()
    {
        // move left and right until close to the player, then transition to approach
        
       

        // patrol speed faster than movement speed
        var step = 1.2f * moveDirection;
        move.Move(step, 0);

        if (readyToLook)
        {
            StartCoroutine(WaitToLookForPlayer());
            print(GetPlayerDistance());
            if(GetPlayerDistance() <= sightRange)
            {
                readyToLook = true;
                StartApproach();
            }
        }
    }
    void StartApproach()
    {
        State = AI.APPROACH;
    }

    void Approach()
    {
        //if (IsPlayerLeft())
          //  moveDirection = -1;

        var direction = transform.position - GameManager.Instance.Player.transform.position;
        var step = direction.normalized * .9f;
        move.Move(step.x, step.y);
    }
    public void StartPatrol()
    {
        // direction assumes spawn on left side, but will change if right
        if (IsPlayerLeft())
            moveDirection = -1;

        State = AI.PATROL;
    }

    float GetPlayerDistance()
    {
        return Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position);
    }

    bool IsPlayerLeft()
    {
        return transform.position.x > GameManager.Instance.Player.transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "HorizontalBoundary")
            moveDirection *= -1;
    }

    IEnumerator WaitToLookForPlayer()
    {
        readyToLook = false;
        yield return new WaitForSeconds(.3f);
        readyToLook = true;
    }

}
