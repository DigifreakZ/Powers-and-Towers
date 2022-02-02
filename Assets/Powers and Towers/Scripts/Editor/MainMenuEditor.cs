using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MainMenu))]
public class MainMenuEditor : Editor
{
    private readonly string[] options = new string[] {"SpellTesting", "TowerTesting"};
    private int selected = 0;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MainMenu mainMenu = (MainMenu)target;
        selected = EditorGUILayout.Popup("Which scene to load?", selected, options);
        mainMenu.sceneToLoad = options[selected];
    }
}
