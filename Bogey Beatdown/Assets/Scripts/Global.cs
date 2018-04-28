using UnityEngine;
using System.Collections;
using System;

public static class Global
{

    public static int StageEnemies = 0;

    public static void FreezeAllCharacters()
    {
        var chars = GameObject.FindObjectsOfType<Character>();
        foreach(var c in chars)
        {
            c.FreezeCharacter();
        }
    }

    public static void UnfreezeAllCharacters()
    {
        var chars = GameObject.FindObjectsOfType<Character>();
        foreach (var c in chars)
        {
            c.UnfreezeCharacter();
        }
    }

    public static void RemoveStageEnemy()
    {
        --StageEnemies;
        if(StageEnemies == 0)
        {
            Camera.main.GetComponent<Movement>().canMove = true;
            GameManager.Instance.UI.ShowMessage("Stage Complete", 2f);
            GameManager.Instance.CurrentStage = null;
        }
    }
}
