using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UpgradeTowerButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Tower selectedTower;
    private Color _color;
    private void Start()
    {
        TowerSelector.instance.upgradeButton = this;
    }
    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
    }
    // Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        _color = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(1f,1f,1f,0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = _color;
    }

    // Clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        if (selectedTower == null) return;

        float yourCurrency = 999999; // Insert Get Currency Function here

        if (selectedTower.data.upgradedVersion.cardCost <= yourCurrency)
        {
            yourCurrency -= selectedTower.data.upgradedVersion.cardCost;
            selectedTower.Upgrade();
        }

        // Return your currency Value Here
        Debug.LogWarning("[Reminder] Need Currency System");
    }
}
