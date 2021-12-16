using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject enemyPrefab;
    public bool nextWave;
    [SerializeField] private Transform[] _pathNodes;
    [SerializeField] private List<int> wave;
    private List<Enemy> enemies;
    private void Awake()
    {
        if (MapManager.instance != null) Destroy(gameObject);
        MapManager.instance = this;
        enemies = new List<Enemy>();
        StartCoroutine("Spawner");
    }
    private void Update()
    {
        if (nextWave)
        {
            nextWave = false;
            StartCoroutine("Spawner");
        }
        else
        {

            //if (enemies.Count <= 0)
            //{
            //    nextWave = true;
            //}
        }
    }
    // spawn enemy at start
    //Instantiate(enemyPrefab, _pathNodes[0].position, Quaternion.identity);

    public static Transform[] GetPath()
    {
        if (MapManager.instance == null)
        {
            Debug.LogWarning("No Map Manager: MapManager.cs");
            return null;
        }

        return instance._pathNodes;
    }
    public void CommandEndRound()
    {
        StopAllCoroutines();
        for (int i = enemies.Count -1; i >= 0; i--)
        {
            try
            {
                enemies[i].Health = 0;
            }
            catch 
            {
                enemies.RemoveAt(i);
            }
        }
    }

    public void CommandStartNextRound()
    {
        nextWave = true;
    }

    IEnumerator Spawner()
    {
        if (wave != null)
        {
            if (wave.Count == 0)
            wave.Add(10);
            else
            wave.Add(wave[wave.Count - 1] * 2);
        }
        else
        {
            wave = new List<int>();
            wave.Add(10);
        }

        for (int i = 0; i < wave[wave.Count - 1]; i++)
        {
            enemies.Add(Instantiate(enemyPrefab, _pathNodes[0].position, Quaternion.identity).GetComponent<Enemy>());
            yield return new WaitForSeconds((10/wave[wave.Count - 1]));
        }
    }

    internal void EnemyDied(Enemy enemy)
    {
        try
        {
            enemies.Remove(enemy);
        }
        catch { }
        return;
    }
}
