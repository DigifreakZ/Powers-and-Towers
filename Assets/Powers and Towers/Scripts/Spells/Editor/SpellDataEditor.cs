using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpellData))]
public class SpellDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpellData spellData = (SpellData)target;

        if (spellData.slow)
        {
            //spellData.slowPower.
        }
    }
}
