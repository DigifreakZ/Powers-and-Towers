using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower",menuName ="Towers/New Tower")]
public class TowerData : ScriptableObject
{
    public float attackSpeed = 1f;
    public int damage = 1;
    public DamageType type;

    public GameObject TowerPrefab;

    public void SetTower(Vector3 pos) 
    {
        Instantiate(TowerPrefab, pos, Quaternion.identity);
    } 
}
