using UnityEngine;

public class SpellData : ScriptableObject
{
    public float spellID;
    public float spellCost;
    public string[] spellNames = new string[3];
    public Sprite[] spellIcons = new Sprite[3];
    public string spellDescription;
    [HideInInspector]
    public LayerMask target;
    public float spellRange;
    public float spellRangeLevelUp;

    public virtual void Cast(int levelCastAt)
    {
        Debug.Log("Cast " + spellNames[levelCastAt]);
        //Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), spellRange + levelCastAt * spellRangeLevelUp, target);
    }
}
