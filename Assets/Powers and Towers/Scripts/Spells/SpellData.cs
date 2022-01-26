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
    public float VisualEffectDuration;
    [HideInInspector] public bool continousSpellEffect;
    [HideInInspector] public int spellDuration;
    [HideInInspector] public SpellType spellType;
    [HideInInspector] public bool doDamage;
    [HideInInspector] public int damageAmount;
    [HideInInspector] public int damageAmountIncrease;
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public bool doDamageOverTime;
    [HideInInspector] public float dOTDamage;
    [HideInInspector] public float dOTDamageIncrease;
    [HideInInspector] public float dOTDuration;
    [HideInInspector] public bool blackHole;
    [HideInInspector] public bool doSlow;
    [HideInInspector] public float slowPower;
    [HideInInspector] public float slowPowerIncrease;
    [HideInInspector] public float slowDuration;
    [HideInInspector] public bool increaseLootValue;
    [HideInInspector] public float lootValueModifier;
    [HideInInspector] public bool doBuff;
    [HideInInspector] public float buffModifier;
    [HideInInspector] public BuffType buffType;
    [HideInInspector] public bool wildMagic;

    public void Cast(int levelCastAt, Vector2 castPoint)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(castPoint, spellRange + levelCastAt * spellRangeLevelUp, target);
        foreach (var item in hits)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            Tower tower = item.GetComponent<Tower>();
            if (increaseLootValue && item.CompareTag("Enemy"))
            {
                enemy.IncreaseLootValue(lootValueModifier);
            }
            if (doDamage && item.CompareTag("Enemy"))
            {
                Debug.Log("Enemy damaged");
                enemy.ReceiveDamage(damageAmount + damageAmountIncrease * levelCastAt, damageType);
            }
            if (doDamageOverTime && item.CompareTag("Enemy"))
            {
                Debug.Log("Started Damage over Time");
                enemy.StartDOTRoutine(dOTDamage + dOTDamageIncrease * levelCastAt, dOTDuration, damageType);
            }
            if (doSlow && item.CompareTag("Enemy"))
            {
                Debug.Log("Enemy slowed");
                enemy.GetSlowed(slowPower + slowPowerIncrease * levelCastAt, slowDuration);
            }
            if (doBuff && item.CompareTag("Tower"))
            {
                //Debug.Log("Tower Buffed");
                tower.ApplyBuff((float)(buffModifier + 0.5 *  levelCastAt), buffType);
            }
            if (blackHole && item.CompareTag("Tower"))
            {
                // Chance to destroy towers
                // 20% at level 1
                // 10% at level 2
                // 0% at level 3
                if (UnityEngine.Random.value < 0.2 - 0.1 * levelCastAt)   
                {
                    tower.DestroyTower(false);
                }
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
[Flags] public enum BuffType
{
    Damage = 1,
    Range = 2,
    AttackSpeed = 4,

    DamageAndRange = Damage | Range,
    RangeAndAttackSpeed = Range | AttackSpeed,
    DamageAndAttackSpeed = Damage | AttackSpeed,
}