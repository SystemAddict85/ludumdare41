using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    public int stageNumber = 1;
    public string message = "Stage Start";
    
    public EnemySpawner[] spawners;
	// Use this for initialization
	void Awake () {
        GetComponent<SpriteRenderer>().enabled = false;
        spawners = GetComponentsInChildren<EnemySpawner>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "StageCollider")
        {
            GameManager.Instance.CurrentStage = this;
            StartCoroutine(StartSpawners());
        }
    }

    IEnumerator StartSpawners()
    {
        GameManager.Instance.UI.ShowMessage(message, 2f);
        yield return new WaitForSeconds(2.1f);
        Global.StageEnemies = 0;
        foreach (var s in spawners)
        {
            s.StartSpawning();
        }

        Camera.main.GetComponent<Movement>().canMove = false;
    }
}
