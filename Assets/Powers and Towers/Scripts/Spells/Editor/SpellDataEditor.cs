using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellData))]
public class SpellDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpellData spellData = (SpellData)target;

        if (spellData.spellType.Contains(SpellType.Damage))
        {
            spellData.damage = EditorGUILayout.Toggle("Damage", spellData.damage);
            if (spellData.damage)
            {
                spellData.damagePower = EditorGUILayout.IntField("Damage Power", spellData.damagePower);
                spellData.damageType = (DamageType)EditorGUILayout.EnumPopup("Damage Type", spellData.damageType);
            }
        }

        if (spellData.spellType.Contains(SpellType.Control))
        {
            spellData.slow = EditorGUILayout.Toggle("Slow", spellData.slow);
            if (spellData.slow)
            {
                spellData.slowPower = EditorGUILayout.Slider("Slow Power", spellData.slowPower, 0, 1);
            }
        }

        if (spellData.spellType.Contains(SpellType.Support))
        {
            spellData.buff = EditorGUILayout.Toggle("Buff", spellData.buff);
            if (spellData.buff)
            {
                spellData.buffType = (BuffType)EditorGUILayout.EnumPopup("Buff Type", spellData.buffType);
            }
        }
    }
}
