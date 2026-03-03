using UnityEngine;

public static class ScoreManager
{
    public static int levelXP;

    public static void AddXP(int xp)
    {
        levelXP += xp;
        if (levelXP > 0)
            Debug.Log("wow");
    }
}
