using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defensive Spell", menuName = "Spells/Defensive spell")]
public class DefensiveSpell : SpellData
{
    [HideInInspector] public bool buff;
    [HideInInspector] public BuffType buffType;
    public DefensiveSpell()
    {
        base.target = 1 << 8;
    }
}

public enum BuffType
{
    All,
    Damage,
    Range,
    Speed
}
