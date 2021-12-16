using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellCard))]
public class SpellCardEditor : Editor
{   
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpellCard spellCard = (SpellCard)target;

        if (GUILayout.Button("Level Up"))
        {
            spellCard.LevelUp();
        }
    }
}
