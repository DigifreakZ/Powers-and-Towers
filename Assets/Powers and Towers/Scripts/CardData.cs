using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cardID = -1;
    public int cardCost;
    public Sprite cardImage;
    [TextArea]
    public string cardDescription;

    private void OnValidate()
    {
        if (cardID == -1)
        {
            CardDataBase database;
            database = AssetDatabase.LoadAssetAtPath("Assets\\Resources\\CardDataBase.asset", typeof(CardDataBase)) as CardDataBase;
            database.cardData.Add(this);
            database.OnValidate();
        }
    }
}
