using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHit : MonoBehaviour {

    Character chara;

    [HideInInspector]
    public bool canBeHit = true;
    

    private void Awake()
    {
        chara = GetComponent<Character>();
    }

    public void TakeAHit(AttackType attack)
    {
        print(gameObject.name + " has been hit by: " + attack.chara.name + " using " + attack.attackName);
        canBeHit = false;

        Vector2 dir = transform.position - attack.chara.transform.position;
        KnockBack(attack.knockBack, dir);

        chara.stats.ChangeHealth(-attack.damage);
        StartCoroutine(Stun(attack.stun));
    }

    public void TakeAHit(float damage, float stunDur, float knockBack, Vector2 position)
    {
        canBeHit = false;

        chara.stats.ChangeHealth(damage);

        StartCoroutine(Stun(stunDur));
    }

    public void KnockBack(float knockBack, Vector2 dir)
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir.normalized * knockBack, ForceMode2D.Impulse);
    }

    public IEnumerator Stun(float dur)
    {
        yield return new WaitForSeconds(dur);
        canBeHit = true;
    }
    
}
