using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Enemy_Database", menuName = "ScriptableObject/Database/Enemy Database")]
public class EnemyDataBase : ScriptableObject
{
    public List<EnemyData> Enemies;

    public void OnValidate()
    {
        WaveData.enemyRange = Enemies.Count;
        if (Enemies.Count <= 0) return;
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].EnemyID = i;
        }
    }
}
