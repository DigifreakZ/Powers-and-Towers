using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "New Offensive Spell", menuName = "Spells/Offensive spell")]

public class OffensiveSpell : SpellData
{
    [HideInInspector] public bool damage;
    [HideInInspector] public int damagePower;
    [HideInInspector] public DamageType damageType;
    [HideInInspector] public bool slow;
    [HideInInspector] public float slowPower;

    public override void Cast(int levelCastAt)
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
        }
    }

    public OffensiveSpell()
    {
        base.target = 1<<7;
    }
}
