using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="Scoreboard Manager")]
public class ScoreManager : ScriptableObject
{
    public int currentScore;
    public int scoreBaseEnemy;
    public int scorePincho;
    public int scoreBigAkita;
    

    private void OnEnable()
    {
        currentScore = 0;
    }

    public void ModifyScore(int x)
    {
        currentScore += x;
        if (currentScore < 0)
            currentScore = 0;
    }

    public void AddScore(Enemy.EnemyType type)
    {
        Debug.Log("Llego hasta los puntajes");
        switch (type)
        {
            case Enemy.EnemyType.enemyBase:
                ModifyScore(scoreBaseEnemy);
                break;
            case Enemy.EnemyType.pincho:
                ModifyScore(scorePincho);
                break;
            case Enemy.EnemyType.bigAkita:
                ModifyScore(scoreBigAkita);
                break;
            
        }
    }
}
