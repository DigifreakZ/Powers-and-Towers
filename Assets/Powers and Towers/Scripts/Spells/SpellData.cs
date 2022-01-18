using System;
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
    public GameObject visualEffect;
    [HideInInspector] public bool continousSpellEffect;
    [HideInInspector] public int spellDuration;
    [HideInInspector] public SpellType spellType;
    [HideInInspector] public bool damage;
    [HideInInspector] public int damagePower;
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public bool damageOverTime;
    [HideInInspector] public float dOTDamage;
    [HideInInspector] public float dOTDuration;
    [HideInInspector] public bool slow;
    [HideInInspector] public float slowPower;
    [HideInInspector] public float slowDuration;
    [HideInInspector] public bool buff;
    [HideInInspector] public float buffModifier;
    [HideInInspector] public BuffType buffType;

    public void Cast(int levelCastAt, Vector2 castPoint)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(castPoint, spellRange + levelCastAt * spellRangeLevelUp, target);
        foreach (var item in hits)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            if (damage)
            {
                Debug.Log("Enemy damaged");
                enemy.ReceiveDamage(damagePower * levelCastAt, damageType);
            }
            if (damageOverTime)
            {
                Debug.Log("Started Damage over Time");
                enemy.StartDOTRoutine(dOTDamage, dOTDuration, damageType);
            }
            if (slow)
            {
                Debug.Log("Enemy slowed");
                item.GetComponent<Enemy>().GetSlowed(slowPower, slowDuration);
            }
            if (buff)
            {
                //Debug.Log("Tower Buffed");
                item.GetComponent<Tower>().ApplyBuff((float)(buffModifier + 0.5 *  levelCastAt));
            }
        }
    }
}


[Flags] public enum SpellType
{
    Damage = 1,
    Control = 2,
    Support = 4,

    DamageAndControl = Damage | Control,
    ControlAndSupport = Control | Support,
    DamageAndSupport = Damage | Support

}
public enum BuffType
{
    All,
    Damage,
    Range,
    Speed
}