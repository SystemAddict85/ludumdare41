using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour {

    BoxCollider2D col;
    List<GameObject> characterHit = new List<GameObject>();

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void StartHit()
    {
        characterHit.Clear();
        col.enabled = true;
    }

    public void EndHit()
    {
        col.enabled = false;
    }


    public List<GameObject> CheckForAttack()
    {
        EndHit();
        return characterHit;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("CharacterHit") 
            && !characterHit.Contains(col.gameObject)
            && col.gameObject != transform.parent.gameObject)
        {
            characterHit.Add(col.gameObject);
        }
    }
}
