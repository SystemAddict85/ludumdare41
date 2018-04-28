using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    private Character chara;
    [Range(1,10)]
    public float health = 5;
    [Range(1, 10)]
    public float maxHealth = 5;
    [Range(1, 10)]
    public float attackDamage = 1;
    [Range(1, 10)]
    public float attackSpeed = 1;
    [Range(1, 10)]
    public float attackKnockBack = 1;
    [Range(1, 10)]
    public float moveSpeed = 1;
    [Range(1, 10)]
    public float defense = 1;
    [Range(1,2)]
    public float recoveryTime = 1f;

    private void Awake()
    {
        chara = GetComponent<Character>();
    }



    public void ChangeHealth(float change, bool ignoringDefense = false)
    {
        if(!ignoringDefense)
            change /= defense;

        if (health + change > maxHealth)
            health = maxHealth;
        else
            health += change;
        
        if (GetComponent<PlayerInput>())
        {
            GameManager.Instance.UI.UpdateHealthBar(health, maxHealth);
        }

        if (CheckForDeath())
            chara.Die();
            
    }

    public bool CheckForDeath()
    {
        return health <= 0;
    }

}
