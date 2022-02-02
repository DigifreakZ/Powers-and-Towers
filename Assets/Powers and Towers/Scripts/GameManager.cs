using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;

    // Singelton Variables
    public int[] DeckCards = new int[6];
    public int[] SpellCards = new int[6];
    [SerializeField] public CardDataBase cardDatabase;
    [SerializeField] public EnemyDataBase enemyDatabase;
    public MapManager mapManager;

    // Ingame Variables
    private int _health = 0;
    private const float MAXMANA = 30;
    private float _mana = 15;
    private bool manaRegenInProgress = false;
    public float manaRegenRate = 1;
    [SerializeField] private int _currency = 0;
    [HideInInspector] public DashBoard DashBoard;

    // Deck Builder Variables
    [HideInInspector] public DeckBuilderHandTower TowerToAdd;
    [HideInInspector] public DeckBuilderHandSpell SpellToAdd;
    [HideInInspector] public int currentHoldID;
    [HideInInspector] public int hoverID;
    public bool holdingCard;
    public bool hoveringDropCard;


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
            if (DashBoard != null)
            UpdateUIDashBoard();
        }
    }
    public float Mana
    {
        get { return _mana; }
        set
        {
            if (value > MAXMANA)
            {
                _mana = MAXMANA;
            }
            else
            {
                _mana = value;
            }
            if (mapManager == null)
            {
                mapManager = FindObjectOfType<MapManager>();
            }
            if (!manaRegenInProgress)
            {
                StartCoroutine(ManaRegen());
            }
            if (DashBoard != null)
                UpdateUIDashBoard();
        }
    }
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                ResetValues();
                SceneManager.LoadScene("Main Menu");
            }
            if (DashBoard != null)
                UpdateUIDashBoard();
        }
    }
    public void DebugGiveCurrency(int currency)
    {
        Currency += currency;
    }

    public GameObject GetEnemyFromID(int ID)
    {
        return enemyDatabase.Enemies[ID].gameObject;
    }

    public EnemyData GetEnemyDataFromID(int ID)
    {
        return enemyDatabase.Enemies[ID];
    }

    public static CardData GetCardData(int index)
    {
        if (instance == null) return null;

        return instance.cardDatabase.cardData[index];
    }
    /// <summary>
    /// Return card for your hand corresponding to hand position
    /// </summary>
    /// <param name="deckHolder">ID</param>
    /// <returns>CardData</returns>
    public static CardData GetHand(int deckHolder)
    {
        if (instance == null) return null;

        return instance.cardDatabase.cardData[instance.DeckCards[deckHolder]];
    }
    public static CardData GetHandS(int deckHolder)
    {
        if (instance == null) return null;

        return instance.cardDatabase.cardData[instance.SpellCards[deckHolder]];
    }

    /// <summary>
    /// Updates DashBoard UI (Health, currency)
    /// </summary>
    private void UpdateUIDashBoard()
    {
        if (DashBoard == null) return;
        DashBoard.CurrencyText = _currency.ToString();
        DashBoard.ManaText = _mana.ToString() + "/" + MAXMANA;
        DashBoard.HealthText = _health.ToString();
    }

    public void ResetValues()
    {
        _health = 100;
        _currency = 0;
    }

    private void Awake()
    {
        if (GameManager.instance != null) Destroy(gameObject);
        else
        {
            GameManager.instance = this;
            DontDestroyOnLoad(this);
            Health = 100;
        }
    }
    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
        UpdateUIDashBoard();
    }

    private IEnumerator ManaRegen()
    {
        manaRegenInProgress = true;
        while (_mana < MAXMANA)
        {
            yield return new WaitForSeconds(1);
            if (mapManager.onGoingWave)
            {
                Mana += manaRegenRate;
            }
        }
        manaRegenInProgress = false;
    }
}
