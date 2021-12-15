using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower",menuName ="Towers/New Tower")]
public class TowerData : ScriptableObject
{
    public float attackSpeed = 1f;
    public int damage = 1;
    public DamageType type;
    public Sprite spriteImage;
    public GameObject TowerPrefab;

    public void SetTower(Vector3 pos) 
    {
        GameObject _object = Instantiate(TowerPrefab, pos, Quaternion.identity);
        Tower towerScript = _object.GetComponent<Tower>();
        towerScript.damage = damage;
    } 
}
