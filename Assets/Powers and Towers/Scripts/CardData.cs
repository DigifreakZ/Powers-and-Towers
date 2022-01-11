using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : ScriptableObject
{
    public string cardName;
    public int cardID;
    public int cardCost;
    public Sprite cardImage;
    [TextArea]
    public string cardDescription;
}
