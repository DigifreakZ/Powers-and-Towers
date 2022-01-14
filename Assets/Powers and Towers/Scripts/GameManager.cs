using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int[] DeckCards;
    private int _currency = 0;
    [SerializeField] private CardDataBase towerData;
    [SerializeField] private EnemyDataBase enemyData;
    public DashBoard dashBoard;
    /// <summary>
    /// Get: return current Currency.
    /// Set: Sets new Currency and Updates UI.
    /// </summary>
    public int Currency
    {
        get { return _currency; }
        set
        {
            _currency = value;
            if (dashBoard != null)
            UpdateUIDashBoard();
        }
    }

    public GameObject GetEnemyFromID(int ID)
    {
        return enemyData.Enemies[ID].gameObject;
    }
    public EnemyData GetEnemyDataFromID(int ID)
    {
        return enemyData.Enemies[ID];
    }
    public static CardData GetCardData(int index)
    {
        if (instance == null) return null;

        return instance.towerData.cardData[index];
    }
    /// <summary>
    /// Return card for your hand corresponding to hand position
    /// </summary>
    /// <param name="deckHolder">ID</param>
    /// <returns>CardData</returns>
    public static CardData GetHand(int deckHolder)
    {
        if (instance == null) return null;

        return instance.towerData.cardData[instance.DeckCards[deckHolder]];
    }
    /// <summary>
    /// Updates UI
    /// </summary>
    private void UpdateUIDashBoard()
    {
        dashBoard.CurrencyText = _currency.ToString();
    }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

}
