using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    AttackBox box;
    Animator anim;

    AttackType currentAttack;

    [HideInInspector]
    public bool canAttack = true;

    public float attackDelay = .5f;


    #region Animation Functions
    public void AttackAnimation()
    {
        box.StartHit();
    }
    public void StopHitBoxAnimation()
    {
        StopHitBox();
    }
    public void EndAttack()
    {
        GetComponent<Movement>().ToggleMove(true);
        StartCoroutine(AttackDelay());
    }
    #endregion

    private void Awake()
    {
        box = GetComponentInChildren<AttackBox>();
        anim = GetComponent<Animator>();
    }

    public void StartAttack(AttackType attack)
    {
        GetComponent<Movement>().ToggleMove(false);
        currentAttack = attack;
        anim.SetTrigger("MainAttack");        
    }

    public void StopHitBox()
    {
        var hits = box.CheckForAttack();

        foreach (var hit in hits)
            hit.GetComponent<CharacterHit>().TakeAHit(currentAttack);
    }    

    public IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay / GetComponent<CharacterStats>().attackSpeed);
        canAttack = true;
    }
    
}

public class AttackType
{
    public Character chara;
    public string attackName;
    public float damage;
    public float stun;
    public float knockBack;


    public AttackType(Character chara, string name, float knockBack, float stun)
    {
        this.attackName = name;
        this.chara = chara;
        this.stun = stun;
        this.knockBack = knockBack;
        this.damage = chara.GetComponent<CharacterStats>().attackDamage;
        
    }

    public AttackType(string name, float damage, float stun, float knockBack)
    {
        this.attackName = name;
        this.damage = damage;
        this.stun = stun;
        this.knockBack = knockBack;
    }
}
