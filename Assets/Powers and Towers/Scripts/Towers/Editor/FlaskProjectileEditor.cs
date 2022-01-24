using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(FlaskProjectile))]
public class FlaskProjectileEditor : Editor
{
    Vector3 endPosition;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FlaskProjectile flask = (FlaskProjectile)target;
        EditorGUILayout.Space();
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField("DamageType", style, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Init"))
        {
            flask.Init(20, DamageType.Fire,5f,endPosition);
        }
        endPosition = EditorGUILayout.Vector3Field("Vector", endPosition);

    }
}
