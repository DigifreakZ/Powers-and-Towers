using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Card_DataBase", menuName ="DataBase/Tower")]
public class CardDataBase : ScriptableObject
{
    public List<CardData> cardData;

    public void OnValidate()
    {
        if (cardData.Count <= 0) return;
        for(int i = 0; i < cardData.Count; i++)
        {
            cardData[i].cardID = i;
        }
    }
}
