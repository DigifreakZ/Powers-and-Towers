using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class SpellData : ScriptableObject
{
    public float spellID;
    public float spellCost;
    public string[] spellNames = new string[3];
    public Sprite[] spellIcons = new Sprite[3];
    public string spellDescription;
    public LayerMask target;
    public int spellPower;
    public float spellRange;
    public bool slow;
    [Range(0,1)][HideInInspector]
    public float slowPower;


    public void Cast(int levelCastAt)
    {
        Debug.Log("Cast " + spellNames[levelCastAt]);
        //float castRange = spellRange + (levelCastAt * 0.5f);
        Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), spellRange + levelCastAt * 0.5f, target);
        foreach (var item in hits)
        {
            Debug.Log("Enemy hit");
            item.GetComponent<Enemy>().Health -= spellPower;
        }
    }
}
