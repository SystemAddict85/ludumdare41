using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour {

    Attack attack;
    BoxCollider2D col;
    List<GameObject> charactersHit = new List<GameObject>();

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        attack = GetComponentInParent<Attack>();
    }

    public void StartHit()
    {
        charactersHit = new List<GameObject>();
        col.isTrigger = false;
        col.isTrigger = true;
        col.enabled = true;
    }

    public void EndHit()
    {
        col.enabled = false;
    }


    public List<GameObject> CheckForAttack()
    {
        EndHit();
        return charactersHit;
    }

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        var hit = otherCol.GetComponent<CharacterHit>();
        if( hit && IsOppositeHitOnly(hit))
        {
            charactersHit.Add(hit.gameObject);
        }
    }

    bool IsOppositeHitOnly(CharacterHit charHit)
    {
        if(!charactersHit.Contains(charHit.gameObject) && col.gameObject != attack.gameObject)
        {
            return (charHit.tag == "Player" && attack.tag == "Enemy")
                || (charHit.tag == "Enemy" && attack.tag == "Player");
        }

        return false;
    }
}
