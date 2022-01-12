using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Spell")]

public class SpellData : CardData
{
    public string[] spellNames = new string[3];
    public Sprite[] spellIcons = new Sprite[3];
    public LayerMask target;
    public float spellRange;
    public float spellRangeLevelUp;
    public List<SpellType> spellType;
    [HideInInspector] public bool damage;
    [HideInInspector] public int damagePower;
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public bool slow;
    [HideInInspector] public float slowPower;
    [HideInInspector] public bool buff;
    [HideInInspector] public BuffType buffType;

    public void Cast(int levelCastAt)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), spellRange + levelCastAt * spellRangeLevelUp, target);
        foreach (var item in hits)
        {
            if (damage)
            {
                Debug.Log("Enemy damaged");
                item.GetComponent<Enemy>().ReceiveDamage(damagePower, damageType);
            }
            if (slow)
            {
                Debug.Log("Enemy slowed");
            }
            if (buff)
            {
                Debug.Log("Tower Buffed");
            }
        }
    }
}

public enum SpellType
{
    Damage,
    Control,
    Support
}
public enum BuffType
{
    All,
    Damage,
    Range,
    Speed
}