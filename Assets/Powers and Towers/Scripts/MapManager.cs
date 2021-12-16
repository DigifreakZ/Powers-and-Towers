using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [SerializeField] private Transform[] _pathNodes;

    private void Awake()
    {
        if (MapManager.instance != null) Destroy(gameObject);
        MapManager.instance = this;
    }
    public static Transform[] GetPath()
    {
        if (MapManager.instance == null)
        {
            Debug.LogWarning("No MapManager: MapManager.cs");
            return null;
        }

        return instance._pathNodes;
    }
}
