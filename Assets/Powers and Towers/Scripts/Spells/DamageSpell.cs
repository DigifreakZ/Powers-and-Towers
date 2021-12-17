using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "New Damage Spell", menuName = "Spells/Damage spell")]

public class DamageSpell : SpellData
{
    public DamageType damageType;

    public override void Cast(int levelCastAt)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), spellRange + levelCastAt * spellRangeLevelUp, target);
        foreach (var item in hits)
        {
            item.GetComponent<Enemy>().ReceiveDamage(spellPower, damageType);
        }
    }
}
