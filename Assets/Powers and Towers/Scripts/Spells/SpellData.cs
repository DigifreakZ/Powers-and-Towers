using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/Spell base class")]
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
    public float spellRangeLevelUp;

    public virtual void Cast(int levelCastAt)
    {
        Debug.Log("Cast " + spellNames[levelCastAt]);
        //Collider2D[] hits = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), spellRange + levelCastAt * spellRangeLevelUp, target);
    }
}
