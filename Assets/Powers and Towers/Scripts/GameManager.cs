using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public int[] DeckCards = new int[6];
    [HideInInspector] public int[] SpellCards = new int[6];
    private int _currency = 0;
    private int _health = 0;
    [SerializeField] public CardDataBase towerData;
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
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (dashBoard != null)
                UpdateUIDashBoard();
        }
    }
    public void DebugGiveCurrency(int currency)
    {
        Currency += currency;
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
        if (dashBoard == null) return;
        dashBoard.CurrencyText = _currency.ToString();
        dashBoard.HealthText = _health.ToString();
    }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        Health = 100;
    }
    private void Start()
    {
        UpdateUIDashBoard();
    }

}
