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
    private void Start()
    {
        GameManager.instance.dashBoard = this;
    }

    public string CurrencyText
    {
        set => currencyText.text = value;
    }
}
