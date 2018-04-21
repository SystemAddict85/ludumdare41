using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Attack))]
public class PlayerInput : MonoBehaviour
{

    Movement move;
    Attack attack;

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
            attack.StartAttack();
        else (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
            move.Move(x, y);
    }
}
