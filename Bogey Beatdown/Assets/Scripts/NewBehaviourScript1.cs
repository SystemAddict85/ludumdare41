using UnityEngine;
using System.Collections;

public static class Global
{
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
}
