using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[System.Serializable]
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    int currencyToAdd = 0;
    SerializedProperty _DeckCards;
    private void OnEnable()
    {
        serializedObject.FindProperty("DeckCards");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        GameManager gameManager = (GameManager)target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Currency"))
        {
            gameManager.DebugGiveCurrency(currencyToAdd);
        }
        currencyToAdd = EditorGUILayout.IntField(currencyToAdd);
        EditorGUILayout.EndHorizontal();
        DeckChooserArea(ref gameManager);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(gameManager);
            EditorSceneManager.MarkSceneDirty(gameManager.gameObject.scene);
        }
    }

    private void DeckChooserArea(ref GameManager gameManager)
    {

        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField("Your Deck", style, GUILayout.ExpandWidth(true));

        //
        // Towers
        //

        EditorGUILayout.LabelField("Towers");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset"))
        {
            for(int i = 0; i < 6; i++)
            {
                gameManager.DeckCards[i] = 0;
            }
        }
        EditorGUILayout.EndHorizontal();
        if (gameManager.towerData != null)
        {
            List<EditorDataHelper> cards = GetTowerCardData(gameManager);
            for (int i = 0; i < 6; i++)
            {
                CardLabelTower(i, cards, ref gameManager);
            }
        }

        //
        // Spells
        //

        EditorGUILayout.LabelField("Spells");
        if (GUILayout.Button("Reset"))
        {
            for (int i = 0; i < 6; i++)
            {
                gameManager.SpellCards[i] = 1;
            }
        }
        if (gameManager.towerData != null)
        {
            List<EditorDataHelper> cards = GetSpellCardData(gameManager);
            for (int i = 0; i < 6; i++)
            {
                CardLabelSpell(i, cards, ref gameManager);
            }
        }

    }

    private void CardLabelTower(int cardHolder, List<EditorDataHelper> cards, ref GameManager gameManager)
    {
        string[] options = new string[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            options[i] = cards[i].cardName;
        }

        GUIContent arrayLabel = new GUIContent($"Card|#{cardHolder +1}");
        int index = 0;
        for(int i = 0; i < cards.Count; i++)
        {
            if (gameManager.DeckCards[cardHolder] == cards[i].cardID)
            {
                index = i;
            }
        }
        index = EditorGUILayout.Popup(arrayLabel, index, options);
        gameManager.DeckCards[cardHolder] = cards[index].cardID;
    }
    private void CardLabelSpell(int cardHolder, List<EditorDataHelper> cards, ref GameManager gameManager)
    {
        string[] options = new string[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            options[i] = cards[i].cardName;
        }

        GUIContent arrayLabel = new GUIContent($"Spell|#{cardHolder + 1}");
        int index = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            if (gameManager.SpellCards[cardHolder] == cards[i].cardID)
            {
                index = i;
            }
        }
        index = EditorGUILayout.Popup(arrayLabel, index, options);
        gameManager.SpellCards[cardHolder] = cards[index].cardID;
    }
    private List<EditorDataHelper> GetTowerCardData(GameManager manager)
    {
        List<EditorDataHelper> options = new List<EditorDataHelper>();
        for (int i = 0; i < manager.towerData.cardData.Count; i++)
        {
            if (manager.towerData.cardData[i].GetType() == typeof(TowerData))
            {
                options.Add(new EditorDataHelper(manager.towerData.cardData[i].cardName, manager.towerData.cardData[i].cardID));
            }
        }
        return options;
    }

    private List<EditorDataHelper> GetSpellCardData(GameManager manager)
    {
        List<EditorDataHelper> options = new List<EditorDataHelper>();
        for (int i = 0; i < manager.towerData.cardData.Count; i++)
        {
            if (manager.towerData.cardData[i].GetType() == typeof(SpellData))
            {
                options.Add(new EditorDataHelper(manager.towerData.cardData[i].cardName, manager.towerData.cardData[i].cardID));
            }
        }
        return options;
    }

    [System.Serializable]
    private class EditorDataHelper
    {
        public string cardName;
        public int cardID;
        public EditorDataHelper(string cardName, int cardID)
        {
            this.cardName = cardName;
            this.cardID = cardID;
        }
    }
}
