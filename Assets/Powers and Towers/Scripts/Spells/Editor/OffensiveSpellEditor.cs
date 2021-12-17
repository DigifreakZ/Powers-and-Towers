using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OffensiveSpell))]
public class OffensiveSpellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        OffensiveSpell spellData = (OffensiveSpell)target;

        spellData.damage = EditorGUILayout.Toggle("Damage", spellData.damage);
        if (spellData.damage)
        {
            spellData.damagePower = EditorGUILayout.IntField("Damage Power", spellData.damagePower);
            spellData.damageType = (DamageType)EditorGUILayout.EnumPopup("Damage Type", spellData.damageType);
        }

        spellData.slow = EditorGUILayout.Toggle("Slow", spellData.slow);
        if (spellData.slow)
        {
            spellData.slowPower = EditorGUILayout.Slider("Slow Power", spellData.slowPower,0,1);
        }
    }
}
