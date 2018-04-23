using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum AI { IDLE, PATROL, APPROACH, SPAWNING };

    public AI State { private set; get; }

    private Movement move;
    private Character chara;

    private int spawnDir = -1;
    private float spawnWalkOffset = 3f;
    private Vector2 spawnPos;

    private int moveDirection = 1;
    private bool readyToLook = true;
    private bool readyToAttack = true;

    public float sightRange = 1f;
    public float attackRange = .4f;
    private float approachOutofRangeCounter;
    public bool canThink = true;

    private void Awake()
    {
        move = GetComponent<Movement>();
        State = AI.IDLE;
        chara = GetComponent<Character>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (canThink)
            MakeDecision();
    }

    void MakeDecision()
    {
        switch (State)
        {
            case AI.IDLE:
                GetComponent<Animator>().SetBool("isWalking", false);
                break;
            case AI.PATROL:
                Patrol();
                break;
            case AI.APPROACH:
                Approach();
                break;
            case AI.SPAWNING:
                Spawning();
                break;
        }
    }

    public void StartSpawn(Vector2 spawnPosition, int spawnDirection)
    {
        this.spawnDir = spawnDirection;
        this.spawnPos = spawnPosition;
        State = AI.SPAWNING;
        spawnWalkOffset += Random.Range(0.1f, 0.4f);
    }
    void Spawning()
    {
        move.Move(0, spawnDir);

        
        if ((spawnDir == -1 && transform.position.y <= spawnPos.y - spawnWalkOffset)
            || (spawnDir == 1 && transform.position.y >= spawnPos.y + spawnWalkOffset))
        {
            StartPatrol();
        }

    }
    void Patrol()
    {
        // patrol speed faster than movement speed
        var step = 1.2f * moveDirection;
        move.Move(step, 0);

        if (readyToLook)
        {
            if (Physics2D.Raycast(transform.position, new Vector2(moveDirection, 0), 1.4f, 1 << LayerMask.NameToLayer("CharacterWall")))
                moveDirection *= -1;

            StartCoroutine(WaitToLookForPlayer());
            if (GetPlayerDistance() <= sightRange)
            {
                StartApproach();
            }
        }
    }
    void StartApproach()
    {
        readyToAttack = true;
        approachOutofRangeCounter = 0f;
        State = AI.APPROACH;
    }

    void Approach()
    {
        //if (IsPlayerLeft())
        //  moveDirection = -1;

        var direction = transform.position - GameManager.Instance.Player.transform.position;
        var step = -direction.normalized * .9f;
        move.Move(step.x, step.y);

        var dist = GetPlayerDistance();
        if (dist > sightRange)
            approachOutofRangeCounter += Time.deltaTime;
        else
            approachOutofRangeCounter = 0;

        if (approachOutofRangeCounter > 2.5f)
        {
            StartPatrol();
            return;
        }
        var attack = GetComponent<Attack>();
        if (readyToAttack && attack.canAttack && dist <= attackRange)
        {
            readyToAttack = false;
            attack.StartAttack(new AttackType(chara, "Swing", chara.stats.attackKnockBack, 1));
            StartCoroutine(DelayAttack());
        }
    }

    IEnumerator DelayAttack()
    {
        State = AI.IDLE;
        yield return new WaitForSeconds(chara.stats.attackSpeed);
        State = AI.APPROACH;
        readyToAttack = true;
    }

    public void StartPatrol()
    {
        moveDirection = 1;
        readyToLook = true;
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.tag == "HorizontalBoundary")
    //        moveDirection *= -1;
    //}

    IEnumerator WaitToLookForPlayer()
    {
        readyToLook = false;
        yield return new WaitForSeconds(.3f);
        readyToLook = true;
    }

}
