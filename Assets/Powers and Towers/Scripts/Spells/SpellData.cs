using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class SpellData : ScriptableObject
{
    public float spellID;
    public float spellCost;
    public string spellNameLevel1;
    public string spellNameLevel2;
    public string spellNameLevel3;
    public Sprite spellIconLevel1;
    public Sprite spellIconLevel2;
    public Sprite spellIconLevel3;
    public string spellDescription;
}
