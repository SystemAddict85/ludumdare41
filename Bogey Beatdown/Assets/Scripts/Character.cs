using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [HideInInspector]
    public CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void Die()
    {
        print(name + " is dead");
        if (GetComponent<PlayerInput>())
        {
            GameManager.GameOver();
        }
    }

    public void FreezeCharacter()
    {
        if (GetComponent<Movement>())
        {
            GetComponent<Movement>().canMove = false;
        }
        if (GetComponent<Attack>())
        {
            GetComponent<Attack>().canAttack = false;
        }
    }
    public void UnfreezeCharacter()
    {
        if (GetComponent<Movement>())
        {
            GetComponent<Movement>().canMove = true;
        }
        if (GetComponent<Attack>())
        {
            GetComponent<Attack>().canAttack = true;
        }
    }
}
