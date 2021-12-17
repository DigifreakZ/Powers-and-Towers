using UnityEngine;

public class SpellData : CardData
{
    public string[] spellNames = new string[3];
    public Sprite[] spellIcons = new Sprite[3];
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
