using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Attack))]
public class PlayerInput : MonoBehaviour
{

    Movement move;
    Attack attack;

    [HideInInspector]
    public GolfBall ball;
    
    private void Awake()
    {
        move = GetComponent<Movement>();
        attack = GetComponent<Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && attack.canAttack)
        {
            attack.canAttack = false;
            var chara = GetComponent<Character>();
            attack.StartAttack(new AttackType(chara,"Swing", chara.stats.attackKnockBack, 1));
        }

        move.Move(x, y);
    }


    // Only component specific to Player so piggyback until need more
    public void StartSwingAnimation(GolfBall golf)
    {
        ball = golf;
        GetComponent<Animator>().Play("GolfSwing");
    }
    
    private void LaunchBall()
    {
        ball.LaunchBall();
    }

    private void StopGolfGameAnimation()
    {
        if(ball.launchPower < 2)
        {
            GetComponent<Character>().UnfreezeCharacter();
        }
    }
}
