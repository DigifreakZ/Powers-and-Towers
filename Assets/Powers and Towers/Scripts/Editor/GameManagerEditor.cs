using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    int currencyToAdd = 100;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Currency"))
        {
            gameManager.DebugGiveCurrency(currencyToAdd);
        }
        currencyToAdd = EditorGUILayout.IntField(currencyToAdd);
        EditorGUILayout.EndHorizontal();

    }
}
