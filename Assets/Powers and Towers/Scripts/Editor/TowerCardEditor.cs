using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TowerCard))]
public class TowerCardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TowerCard towerCard = (TowerCard)target;

        if (GUILayout.Button("Level Up"))
        {
            towerCard.LevelUp();
        }
    }
}

