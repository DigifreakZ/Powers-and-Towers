using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DefensiveSpell))]
public class DefensiveSpellEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DefensiveSpell spellData = (DefensiveSpell)target;

        spellData.buff = EditorGUILayout.Toggle("Buff", spellData.buff);
        if (spellData.buff)
        {
            spellData.buffType = (BuffType)EditorGUILayout.EnumPopup("Buff Type", spellData.buffType);
        }
    }
}
