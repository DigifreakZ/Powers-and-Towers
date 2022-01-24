using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellData))]
public class SpellDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpellData spellData = (SpellData)target;
        EditorGUILayout.BeginHorizontal();
        spellData.continousSpellEffect = EditorGUILayout.Toggle("Continous Spell Effect", spellData.continousSpellEffect);
        if (spellData.continousSpellEffect)
        {
            EditorGUIUtility.labelWidth = 90;
            spellData.spellDuration = EditorGUILayout.IntField("Effect Duration", spellData.spellDuration, GUILayout.MaxWidth(Screen.width));
            EditorGUIUtility.labelWidth = 0;
        }
        EditorGUILayout.EndHorizontal();

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

        /*
            Snippets for making new fields:
            new GUIContent("label", "Tooltip")

        */

        if (spellData.spellType.HasFlag(SpellType.Damage))
        {
            EditorGUILayout.LabelField("Damage Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.doDamage = EditorGUILayout.Toggle("Damage", spellData.doDamage);
            if (spellData.doDamage)
            {
                EditorGUILayout.BeginVertical("Box");

                spellData.damageAmount = EditorGUILayout.IntField(new GUIContent("Damage amount", "How much damage should the spell do"), spellData.damageAmount);
                spellData.damageAmountIncrease = EditorGUILayout.IntField(new GUIContent("Damage amount increase", "How much should the damage increase per spell level"), spellData.damageAmountIncrease);
                spellData.damageType = (DamageType)EditorGUILayout.EnumPopup(new GUIContent("Damage type", "What type of damage does the spell do"), spellData.damageType);
                spellData.doDamageOverTime = EditorGUILayout.Toggle(new GUIContent("Damage over time", "Should the spell do damage over time"), spellData.doDamageOverTime);
                if (spellData.doDamageOverTime)
                {
                    EditorGUILayout.BeginVertical("Box");
                    spellData.dOTDamage = EditorGUILayout.FloatField(new GUIContent("DOT amount", "How much damage should the spell do every second"), spellData.dOTDamage);
                    spellData.dOTDamageIncrease = EditorGUILayout.FloatField(new GUIContent("DOT amount increase", "How much DOT amount increase per level"), spellData.dOTDamageIncrease);
                    spellData.dOTDuration = EditorGUILayout.FloatField(new GUIContent("DOT duration", "How long should the effect last"), spellData.dOTDuration);
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
            }
            spellData.blackHole = EditorGUILayout.Toggle(new GUIContent("Black Hole", "Should the spell destroy towers"), spellData.blackHole);
            

            EditorGUILayout.EndVertical();
        }

        if (spellData.spellType.HasFlag(SpellType.Control))
        {
            EditorGUILayout.LabelField("Control Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.doSlow = EditorGUILayout.Toggle("Slow", spellData.doSlow);
            if (spellData.doSlow)
            {
                EditorGUILayout.BeginVertical("Box");
                spellData.slowPower = EditorGUILayout.Slider(new GUIContent("Slow amount", "How much should targets be slowed by"), spellData.slowPower, 0, 1);
                spellData.slowPowerIncrease = EditorGUILayout.Slider(new GUIContent("Slow amount increase", "How much should slow amount be increase by per level"), spellData.slowPowerIncrease, 0, 0.5f);
                spellData.slowDuration = EditorGUILayout.Slider(new GUIContent("Slow duration", "How long should targets be slowed for"), spellData.slowDuration, 1, 20);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }

        if (spellData.spellType.HasFlag(SpellType.Support))
        {
            EditorGUILayout.LabelField("Support Options", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("Box");
            spellData.doBuff = EditorGUILayout.Toggle("Buff", spellData.doBuff);
            if (spellData.doBuff)
            {
                EditorGUILayout.BeginVertical("Box");
                spellData.buffModifier = EditorGUILayout.Slider(new GUIContent("Buff modifier", "How much should stats be increased by"), spellData.buffModifier, 1, 3);
                spellData.buffType = (BuffType)EditorGUILayout.EnumFlagsField(new GUIContent("Buff type", "What stat should the buff increase"), spellData.buffType);
                EditorGUILayout.EndVertical();
            }
            spellData.increaseLootValue = EditorGUILayout.Toggle("Increase loot value", spellData.increaseLootValue);
            if (spellData.increaseLootValue)
            {
                EditorGUILayout.BeginVertical("Box");
                spellData.lootValueModifier = EditorGUILayout.Slider(new GUIContent("Loot value modifier", "How much should loot value be increased by"), spellData.lootValueModifier, 1, 5);
                EditorGUILayout.EndVertical();
            }
            spellData.wildMagic = EditorGUILayout.Toggle(new GUIContent("Wild magic", "Is the spell wild magic"), spellData.wildMagic);
            EditorGUILayout.EndVertical();
        }
    }
}
