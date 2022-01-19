using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    //
    public static MapManager instance;
    //
    public bool nextWave;

    public GameObject youWonText;
    //
    [SerializeField] private EnemyDataBase database;
    [SerializeField] private Transform[] _pathNodes;
    [SerializeField] private List<Wave> wave;
    //
    private int nextWaveID = -1;
    private List<Enemy> enemies;
    public bool demoMode;
    private void Awake()
    {
        if (MapManager.instance != null) Destroy(gameObject);
        MapManager.instance = this;
        enemies = new List<Enemy>();
        //StartCoroutine("Spawner");
    }
    private void Start()
    {
        GameManager.instance.DebugGiveCurrency(10);
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
    private bool onGoingWave;
    public void CommandStartNextRound()
    {
        if (onGoingWave) return;
        nextWave = true;
        onGoingWave = true;
        if (wave.Count != nextWaveID + 1)
        nextWaveID += 1;
        if (GameManager.instance != null)
        {
            GameManager.instance.dashBoard.CurrentWave = (1+nextWaveID).ToString();
            GameManager.instance.dashBoard.MaxWave = wave.Count.ToString();
        }
    }

    private float timeBetweenSpawning = 0.2f;
    IEnumerator Spawner()
    {
        if (wave == null) Debug.LogError("No wave");
        List<WaveData> currentWaveData = wave[nextWaveID].waveData;
        for(int whatWave = 0; whatWave < currentWaveData.Count; whatWave++)
        {
            timeBetweenSpawning = currentWaveData[whatWave].TB;
            for (int i = 0; i < currentWaveData[whatWave].NR; i++)
            {
                yield return new WaitForSeconds(timeBetweenSpawning);
                enemies.Add(Instantiate(GameManager.instance.GetEnemyFromID(currentWaveData[whatWave].ID), _pathNodes[0].position, Quaternion.identity).GetComponent<Enemy>());
                enemies[enemies.Count - 1].EnemyData = GameManager.instance.GetEnemyDataFromID(currentWaveData[whatWave].ID);
            }
        }
        yield return null;
    }

    internal void EnemyDied(Enemy enemy)
    {
        try
        {
            enemies.Remove(enemy);
        }
        catch { }

        if (onGoingWave && enemies.Count <= 0)
        {
            onGoingWave = false;
            if (nextWaveID + 1 == wave.Count)
            {
                StartCoroutine("YouWon");
            }
        }
        return;
    }
    IEnumerator YouWon()
    {
        youWonText.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Main Menu");
    }
    private void OnValidate()
    {
        for(int first = 0; first < wave.Count; first++)
        {
            wave[first].name = $"Wave#{first +1}";
            for (int second = 0; second < wave[first].waveData.Count; second++)
            {
                //print($"{first}_{second}");
                try
                {
                    EnemyData data = database.Enemies[wave[first].waveData[second].ID];
                    wave[first].waveData[second].name = $"{data._name} | {wave[first].waveData[second].NR}";
                }
                catch 
                {
                    wave[first].waveData[second].name = $"No Enemy with |ID#{wave[first].waveData[second].ID}|";
                }
            }
        }
    }
}


[Serializable]
public class Wave
{
    public string name;
    public List<WaveData> waveData;
}

[Serializable]
public class WaveData
{

    [HideInInspector]
    static public int enemyRange;
    [Header("Name-# of Spawns")]
    public string name;
    /// <summary>
    /// Number of enemies to spawn.
    /// </summary>
    [Range(1,10000)][Header("Number of Enemies")]
    public int NR; // Number of Enemies
    /// <summary>
    /// Enemy ID will determan what enemy to spawn
    /// </summary>
    //[Range(0, enemyRange)]
    [Header("ID of Enemy")]
    public int ID; // Enemy Type
    /// <summary>
    /// Time Between next Spawning
    /// </summary>
    [Header("Time Between Spawning")]
    public float TB;
}

