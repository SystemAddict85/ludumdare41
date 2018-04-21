using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    AttackBox box;
    Animator anim;

    public bool canAttack = true;

    public void AttackAnimation()
    {
        box.StartHit();
    }
    public void StopHitBoxAnimation()
    {
        EndAttack();
    }

    private void Awake()
    {
        box = GetComponentInChildren<AttackBox>();
        anim = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        canAttack = false;
        anim.SetTrigger("MainAttack");
    }

    public void EndAttack()
    {
        var hits = box.CheckForAttack();

        foreach (var hit in hits)
            print(hit.name);

        canAttack = true;
    }
}
