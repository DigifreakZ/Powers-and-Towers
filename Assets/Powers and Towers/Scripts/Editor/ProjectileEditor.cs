using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Projectile spellData = (Projectile)target;
        EditorGUILayout.Space();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField("DamageType", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Single"))
        {
        }
        if (GUILayout.Button("Control"))
        {
        }
        EditorGUILayout.EndHorizontal();
    }
}
