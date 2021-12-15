using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject enemyPrefab;
    [SerializeField] private Transform[] _pathNodes;

    private void Awake()
    {
        if (MapManager.instance != null) Destroy(gameObject);
        MapManager.instance = this;
        StartCoroutine("Spawner");
    }
    public static Transform[] GetPath()
    {
        if (MapManager.instance == null)
        {
            Debug.LogWarning("No Map Manager: MapManager.cs");
            return null;
        }

        return instance._pathNodes;
    }
    IEnumerator Spawner()
    {
        Instantiate(enemyPrefab, _pathNodes[0].position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
    }
}
