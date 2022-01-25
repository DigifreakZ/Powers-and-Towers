using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UpgradeTowerButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Tower selectedTower;
    private Color _color;
    private Image fullCardImage;
    [SerializeField] private Image art;
    [Header("New Values")]
    [SerializeField] TextMeshProUGUI costTextNew;
    [SerializeField] TextMeshProUGUI nameTextNew;
    [SerializeField] TextMeshProUGUI attackTextNew;
    [SerializeField] TextMeshProUGUI speedTextNew;
    [SerializeField] TextMeshProUGUI rangeTextNew;
    [SerializeField] TextMeshProUGUI elementTextNew;
    [Header("Old Values")]
    [SerializeField] TextMeshProUGUI attackTextOld;
    [SerializeField] TextMeshProUGUI speedTextOld;
    [SerializeField] TextMeshProUGUI rangeTextOld;
    [SerializeField] TextMeshProUGUI elementTextOld;
    [Header("-----------")]
    [SerializeField] GameObject ParentOfValues;
    private void Start()
    {
        TowerSelector.instance.upgradeButton = this;
    }
    public void SelectTower(Tower tower)
    {

        if (fullCardImage == null)
        {
            fullCardImage = GetComponent<Image>();
        }
        selectedTower = tower;
        if (tower.data.upgradedVersion == null ||tower.data == tower.data.upgradedVersion)
        {
            fullCardImage.enabled = false;
            ParentOfValues.SetActive(false);
        }
        else
        {

            fullCardImage.enabled = true;
            ParentOfValues.SetActive(true);

            TowerData newTowerData = tower.data.upgradedVersion;
            TowerData oldTowerData = tower.data;

            art.sprite = newTowerData.spriteImage;

            costTextNew.text = newTowerData.cardCost.ToString();
            nameTextNew.text = newTowerData.cardName.ToString();
            attackTextNew.text = newTowerData.damage.ToString();
            speedTextNew.text = (1 / newTowerData.attackCooldown).ToString();
            rangeTextNew.text = newTowerData.range.ToString();
            elementTextNew.text = newTowerData.type.ToString();


            attackTextOld.text = oldTowerData.damage.ToString();
            speedTextOld.text = (1 / oldTowerData.attackCooldown).ToString();
            rangeTextOld.text = oldTowerData.range.ToString();
            elementTextOld.text = oldTowerData.type.ToString();
        }
    }
    // Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fullCardImage == null)
        {
            fullCardImage = GetComponent<Image>();
        }
        _color = fullCardImage.color;
        fullCardImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fullCardImage.color = _color;
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

        SelectTower(selectedTower);
        // Return your currency Value Here
        //Debug.LogWarning("[Reminder] Need Currency System");
    }
}
