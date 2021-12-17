using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Tower",menuName ="Towers/New Tower")]
public class TowerData : CardData
{
    [Tooltip("Tower Damage per Shot")]
    public int damage = 1;
    [Tooltip("Second Between attacks")]
    public float attackSpeed = 1f;
    [Tooltip("Towers attack range")]
    public float range = 5f;
    [Tooltip("What Damage the tower will make")]
    public DamageType type;
    [Tooltip("Next Upgrade of Tower")]
    public TowerData upgradedVersion;
    [Space]
    [Tooltip("Image Player will see when placing tower")]
    public Sprite spriteImage;
    [Tooltip("The Tower that will spawn")]
    public GameObject TowerPrefab;
    /// <summary>
    /// Places Tower
    /// </summary>
    /// <param name="pos"></param>
    public virtual void SetTower(Vector3 pos) 
    {
        GameObject _object = Instantiate(TowerPrefab, pos, Quaternion.identity);
        Tower towerScript = _object.GetComponent<Tower>();
        towerScript.Init(this);
    }
    public virtual void UpgradeTower(Tower tower)
    {
        tower.Init(upgradedVersion);
    }
}

