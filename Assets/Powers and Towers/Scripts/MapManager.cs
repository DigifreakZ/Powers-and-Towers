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
        while(enemies.Count > 0)
        {
            enemies[0].Health = 0;
            enemies.RemoveAt(0);
        }
    }

    public void CommandStartNextRound()
    {
        if (wave != null)
        wave.Add(wave[wave.Count - 1] * 2);
        else
        {
            wave = new List<int>();
            wave.Add(10);
        }
        nextWave = true;
    }

    IEnumerator Spawner()
    {
        if (wave != null)
            wave.Add(wave[wave.Count - 1] * 2);
        else
        {
            wave = new List<int>();
            wave.Add(10);
        }
        for (int i = 0; i < wave[wave.Count - 1]; i++)
        {
            enemies.Add(Instantiate(enemyPrefab, _pathNodes[0].position, Quaternion.identity).GetComponent<Enemy>());
            yield return new WaitForSeconds(0.3f);
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
