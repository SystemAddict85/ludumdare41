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
            GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f, 0f);
            GameManager.Instance.GameOver("You lose");
        }
        else
        {
            Global.RemoveStageEnemy();
            Destroy(this.gameObject, .5f);
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
        if (GetComponent<EnemyAI>())
        {
            GetComponent<EnemyAI>().canThink = false;
        }
        if (GetComponent<PlayerInput>())
            GameManager.Instance.GolfBall.canPlayGolf = false;
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
        if (GetComponent<EnemyAI>())
        {
            GetComponent<EnemyAI>().canThink = true;
        }
        if (GetComponent<PlayerInput>())
            GameManager.Instance.GolfBall.canPlayGolf = true;
    }
}
