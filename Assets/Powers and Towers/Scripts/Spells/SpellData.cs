using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class SpellData : ScriptableObject
{
    public float spellID;
    public float spellCost;
    public string[] spellNames = new string[3];
    public Sprite[] spellIcons = new Sprite[3];
    public string spellDescription;
    public LayerMask target;
    public float spellPower;
    public bool slow;
    [Range(0,1)][HideInInspector]
    public float slowPower;


    public void Cast(int levelCastAt)
    {
        Debug.Log("Cast " + spellNames[levelCastAt]);
    }

}
