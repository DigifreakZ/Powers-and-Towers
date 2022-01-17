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

        EditorGUILayout.LabelField("Toggle Spell Types", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Damage"))
        {
            spellData.spellType ^= SpellType.Damage;
        }
        if (GUILayout.Button("Control"))
        {
            spellData.spellType ^= SpellType.Control;
        }
        if (GUILayout.Button("Support"))
        {
            spellData.spellType ^= SpellType.Support;
        }
        EditorGUILayout.EndHorizontal();

        if (spellData.spellType.HasFlag(SpellType.Damage))
        {
            EditorGUILayout.LabelField("Damage Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.damage = EditorGUILayout.Toggle("Damage", spellData.damage);
            if (spellData.damage)
            {
                spellData.damagePower = EditorGUILayout.IntField("Damage Power", spellData.damagePower);
                spellData.damageType = (DamageType)EditorGUILayout.EnumPopup("Damage Type", spellData.damageType);
                spellData.damageOverTime = EditorGUILayout.Toggle("Damage Over Time", spellData.damageOverTime);
                if (spellData.damageOverTime)
                {
                    spellData.dOTDamage = EditorGUILayout.FloatField("Damage Per Second", spellData.dOTDamage);
                    spellData.dOTDuration = EditorGUILayout.FloatField("DOT Duration", spellData.dOTDuration);
                }
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
                spellData.slowDuration = EditorGUILayout.Slider("Slow Duration", spellData.slowDuration, 1, 20);
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
