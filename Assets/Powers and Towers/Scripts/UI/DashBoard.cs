using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class DashBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI currentWave;
    [SerializeField] private TextMeshProUGUI maxWave;
    private void Start()
    {
        GameManager.instance.DashBoard = this;
        GameManager.instance.Currency = GameManager.instance.Currency;
    }

    public string CurrencyText
    {
        set => currencyText.text = value;
    }
    public string ManaText
    {
        set => manaText.text = value;
    }
    public string HealthText
    {
        set => healthText.text = value;
    }
    public string MaxWave
    {
        set => maxWave.text = value;
    }
    public string CurrentWave
    {
        set => currentWave.text = value;
    }
}
