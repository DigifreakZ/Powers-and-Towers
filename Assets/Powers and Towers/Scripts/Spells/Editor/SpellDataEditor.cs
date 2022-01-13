using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellData))]
public class SpellDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpellData spellData = (SpellData)target;

        EditorGUILayout.Space();
        if (spellData.spellType.HasFlag(SpellType.Damage))
        {
            EditorGUILayout.LabelField("Damage Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.damage = EditorGUILayout.Toggle("Damage", spellData.damage);
            if (spellData.damage)
            {
                spellData.damagePower = EditorGUILayout.IntField("Damage Power", spellData.damagePower);
                spellData.damageType = (DamageType)EditorGUILayout.EnumPopup("Damage Type", spellData.damageType);
            }
            EditorGUILayout.EndVertical();
        }

        if (spellData.spellType.HasFlag(SpellType.Control))
        {
            EditorGUILayout.LabelField("Control Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.slow = EditorGUILayout.Toggle("Slow", spellData.slow);
            if (spellData.slow)
            {
                spellData.slowPower = EditorGUILayout.Slider("Slow Power", spellData.slowPower, 0, 1);
            }
            EditorGUILayout.EndVertical();
        }

        if (spellData.spellType.HasFlag(SpellType.Support))
        {
            EditorGUILayout.LabelField("Support Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.buff = EditorGUILayout.Toggle("Buff", spellData.buff);
            if (spellData.buff)
            {
                spellData.buffType = (BuffType)EditorGUILayout.EnumPopup("Buff Type", spellData.buffType);
            }
            EditorGUILayout.EndVertical();
        }
    }
}
